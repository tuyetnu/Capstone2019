using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.ContractEntities;
using DormyWebService.Entities.ParamEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Repositories;
using DormyWebService.Services.ParamServices;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.ResolveRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomTransfer.SendRoomTransfer;
using DormyWebService.ViewModels.TicketViewModels.RoomTransferRequestViewModels.ResolveRoomTransferRequest;
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
                if (transfers.Exists(b => b.Status == RequestStatus.Pending))
                {
                    throw new HttpStatusCodeException(HttpStatusCode.Forbidden, "RoomTransferService: There are already active transfer requests for this account");
                }
            }

            //TODO: check if contract is valid

            //Create new room reansfer from request
            var result = SendRoomTransferRequest.NewEntityFromRequest(request);

            //Create in database
            result = await _repoWrapper.RoomTransfer.CreateAsync(result);
            return new SendRoomTransferRespone()
            {
                RoomTransferFormId = result.RoomTransferRequestFormId
            };
        }

        public async Task<ResolveRoomTransferResponse> ResolveRequest(ResolveRoomTransferRequest request)
        {
            //Check if this staff exist
            var staff = await _repoWrapper.Staff.FindByIdAsync(request.StaffId);

            if (staff == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "RoomTransferService: Provide StaffId is not a staff");
            }

            //Check if Room Transfer Request Exists
            var roomTransfer = await FindById(request.RoomTransferRequestFormId);

            if (roomTransfer.Status == request.Status)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "RoomTransferService: This request already has this status");
            }

            if (roomTransfer.Status == RequestStatus.Complete)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "RoomTransferService: This request is already completed, can't change it to anything else''");
            }

            //Update information into DataSet
            roomTransfer = _mapper.Map(request, roomTransfer);

            //Update Last Updated Date
            roomTransfer.LastUpdated = DateTime.Now;

            //Update to database
            roomTransfer =
                    await _repoWrapper.RoomTransfer.UpdateAsyncWithoutSave(roomTransfer, roomTransfer.RoomTransferRequestFormId);

            //If request is changed to complete
            if (request.Status == RequestStatus.Complete)
            {
                //Get contract that have the same studentId
                System.Diagnostics.Debug.WriteLine("roomTransfer.StudentId: " + roomTransfer.StudentId);
                var tempStudent = await
                    _repoWrapper.Student.FindAllAsyncWithCondition(c => c.StudentId == roomTransfer.StudentId);
                //TODO change student's room
            }

            //Save to database at once
            //await _repoWrapper.Save();

            //Return mapped response
            //return _mapper.Map<ResolveRoomBookingResponse>(roomTransfer);
            return null;
        }

        public async Task<bool> DeleteRoomTransfer(int id)
        {
            return await _repoWrapper.RoomTransfer.DeleteAsync(await FindById(id)) > 0;
        }
    }
}