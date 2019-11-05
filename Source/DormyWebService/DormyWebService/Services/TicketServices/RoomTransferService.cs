using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.ContractEntities;
using DormyWebService.Entities.ParamEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Repositories;
using DormyWebService.Services.ParamServices;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.EquipmentViewModels.GetEquipment;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.ResolveRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomTransfer.GetRoomTransfer;
using DormyWebService.ViewModels.TicketViewModels.RoomTransfer.SendRoomTransfer;
using DormyWebService.ViewModels.TicketViewModels.RoomTransferRequestViewModels.ResolveRoomTransferRequest;
using Hangfire;
using Sieve.Models;
using Sieve.Services;

namespace DormyWebService.Services.TicketServices
{
    public class RoomTransferService : IRoomTransferService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private IMapper _mapper;
        private ISieveProcessor _sieveProcessor;
        private readonly IStudentService _studentService;
        private readonly IParamService _paramService;
        private readonly IUserService _userService;

        public RoomTransferService(IRepositoryWrapper repoWrapper, IMapper mapper, ISieveProcessor sieveProcessor, IStudentService studentService, IParamService paramService, IUserService userService)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
            _studentService = studentService;
            _paramService = paramService;
            _userService = userService;
        }

        public async Task<RoomTransferRequestForm> FindById(int id)
        {
            var result = await _repoWrapper.RoomTransfer.FindByIdAsync(id);

            if (result == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomTransferService: Room Transfer is not found");
            }

            return result;
        }

        public async Task<AdvancedGetRoomTransferResponse> AdvancedGetRoomTransfer(string sorts, string filters, int? page, int? pageSize)
        {
            var sieveModel = new SieveModel()
            {
                PageSize = pageSize,
                Sorts = sorts,
                Page = page,
                Filters = filters
            };

            var roomTransfers = await _repoWrapper.RoomTransfer.FindAllAsync();

            if (roomTransfers == null || roomTransfers.Any() == false)
            {
                //return null if no equipment is found
                return new AdvancedGetRoomTransferResponse()
                {
                    ResultList = null,
                    CurrentPage = 1,
                    TotalPage = 1
                };
            }

            var resultResponses = new List<GetRoomTransferResponse>();
            var roomTypes = (List<Param>) await _paramService.FindAllParamEntitiesByParamType(GlobalParams.ParamTypeRoomType);

            foreach (var roomTransfer in roomTransfers)
            {
                var student = await _repoWrapper.Student.FindByIdAsync(roomTransfer.StudentId);
                Room room = null;
                if (roomTransfer.RoomId != null)
                {
                    room = await _repoWrapper.Room.FindByIdAsync(roomTransfer.RoomId.Value);
                }

                resultResponses.Add(GetRoomTransferResponse.ResponseFromEntity(roomTransfer,student,room,roomTypes.Find(t=>t.ParamId == roomTransfer.TargetRoomType)));
            }

            //Apply filter, sort
            var result = _sieveProcessor.Apply(sieveModel, resultResponses.AsQueryable(), applyPagination: false).ToList();

            var response = new AdvancedGetRoomTransferResponse()
            {
                CurrentPage = page ?? 1,
                TotalPage = (int)Math.Ceiling((double)result.Count / pageSize ?? 1),
                //Apply pagination
                ResultList = _sieveProcessor
                    .Apply(sieveModel, result.AsQueryable(), applyFiltering: false, applySorting: false).ToList()
            };

            //Return List of result
            return response;
        }

        public async Task<SendRoomTransferRespone> SendRequest(SendRoomTransferRequest request)
        {
            var roomTypes = await _paramService.FindAllByParamType(GlobalParams.ParamTypeRoomType);

            if (!roomTypes.Exists(t=>t.ParamId == request.TargetRoomType))
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomTransferService: Room Type is not found");
            }

            //Find student in database
            var student = await _studentService.FindById(request.StudentId);
            if (student.RoomId == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomTransferService: Student doesn't have any room'");
            }

            //Check if there's active room transfer request
            var transfers = (List<RoomTransferRequestForm>)
                await _repoWrapper.RoomTransfer.FindAllAsyncWithCondition(r => r.StudentId == request.StudentId);
            if (transfers != null)
            {
                if (transfers.Exists(b => b.Status == RequestStatus.Pending || b.Status == RequestStatus.Approved))
                {
                    throw new HttpStatusCodeException(HttpStatusCode.Forbidden, "RoomTransferService: There are already active transfer requests for this account");
                }
            }

            //Check if contract is active next month
            var thisTimeNextMonth = DateTime.Now.AddHours(GlobalParams.TimeZone).AddMonths(1);
            var contracts = (List<Contract>) await _repoWrapper.Contract.FindAllAsyncWithCondition(c => c.StudentId == student.StudentId && c.Status == ContractStatus.Active && thisTimeNextMonth <= c.EndDate);

            if (contracts == null || !contracts.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.Forbidden, "RoomTransferService: Contract isn't active next month");
            }

            var maxDayForApproveRoomTransfer = await _repoWrapper.Param.FindByIdAsync(GlobalParams.MaxDayForApproveRoomTransfer);
            if (maxDayForApproveRoomTransfer?.Value == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomTransferService: MaxDayForApproveRoomTransfer not found");
            }

            //Create new room transfer from request
            var result = SendRoomTransferRequest.NewEntityFromRequest(request, maxDayForApproveRoomTransfer.Value.Value);

            //Create in database
            result = await _repoWrapper.RoomTransfer.CreateAsync(result);
            return new SendRoomTransferRespone()
            {
                RoomTransferFormId = result.RoomTransferRequestFormId
            };
        }

        [AutomaticRetry(Attempts = 3)]
        public async Task<bool> AutoRejectRoomTransfer()
        {
            var now = DateTime.Now.AddHours(GlobalParams.TimeZone);

            //Get all pending room transfer requests that has expired
            var roomTransfers = (List<RoomTransferRequestForm>)await
                _repoWrapper.RoomTransfer.FindAllAsyncWithCondition(r => r.Status == RequestStatus.Pending && now > r.RejectDate);

            //If there aren't any requests, return false
            if (roomTransfers == null || !roomTransfers.Any())
            {
                return false;
            }

            //If there are pending requests
                foreach (var roomTransfer in roomTransfers)
                {
                        roomTransfer.LastUpdated = DateTime.Now.AddHours(GlobalParams.TimeZone);
                        roomTransfer.Status = RequestStatus.Rejected;
                        roomTransfer.Reason = GlobalParams.DefaultAutoRejectRoomTransferReason;
                }

                //save
                await _repoWrapper.Save();

            return true;
        }

        [AutomaticRetry(Attempts = 3)]
        public async Task<bool> AutoCompleteRoomTransfer()
        {
            //Get time for now
            var now = DateTime.Now.AddHours(GlobalParams.TimeZone);

            //Get all approved room transfer that 
            var roomTransfers = (List<RoomTransferRequestForm>)
                await _repoWrapper.RoomTransfer.FindAllAsyncWithCondition(r => r.Status == RequestStatus.Approved && now >= r.TransferDate);

            //Return false if there isn't any approved room transfer
            if (roomTransfers == null || !roomTransfers.Any())
            {
                return false;
            }

            foreach (var roomTransfer in roomTransfers)
            {
                //Get student
                var student = await _repoWrapper.Student.FindByIdAsync(roomTransfer.StudentId);

                //if student or room transfer's room id are empty, continue to next room transfer
                if (student?.RoomId == null || roomTransfer.RoomId == null) continue;

                //Get current room and next room
                var currentRoom = await _repoWrapper.Room.FindByIdAsync(student.RoomId.Value);
                var nextRoom = await _repoWrapper.Room.FindByIdAsync(roomTransfer.RoomId.Value);

                //Update new data
                student.RoomId = nextRoom.RoomId;
                currentRoom.CurrentNumberOfStudent--;
                nextRoom.CurrentNumberOfStudent++;
                roomTransfer.LastUpdated = DateTime.Now.AddHours(GlobalParams.TimeZone);
                roomTransfer.Status = RequestStatus.Complete;

                //Pend all updates
                await _repoWrapper.Student.UpdateAsyncWithoutSave(student, student.StudentId);
                await _repoWrapper.Room.UpdateAsyncWithoutSave(currentRoom, currentRoom.RoomId);
                await _repoWrapper.Room.UpdateAsyncWithoutSave(nextRoom, nextRoom.RoomId);
                await _repoWrapper.RoomTransfer.UpdateAsyncWithoutSave(roomTransfer,
                    roomTransfer.RoomTransferRequestFormId);
            }

            //save
            await _repoWrapper.Save();

            return true;
        }
    }
}