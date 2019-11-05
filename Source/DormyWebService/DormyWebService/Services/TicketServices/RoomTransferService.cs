using System;
using System.Collections.Generic;
using System.Linq;
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
                if (transfers.Exists(b => b.Status == RequestStatus.Pending || b.Status == RequestStatus.Approved))
                {
                    throw new HttpStatusCodeException(HttpStatusCode.Forbidden, "RoomTransferService: There are already active transfer requests for this account");
                }
            }

            //TODO: check if contract is valid

            //Create new room transfer from request
            var result = SendRoomTransferRequest.NewEntityFromRequest(request);

            //Create in database
            result = await _repoWrapper.RoomTransfer.CreateAsync(result);
            return new SendRoomTransferRespone()
            {
                RoomTransferFormId = result.RoomTransferRequestFormId
            };
        }

//        public async Task<bool> AutoRejectRoomTransfer()
//        {
//            var roomTransfers = (List<RoomTransferRequestForm>)await
//                _repoWrapper.RoomTransfer.FindAllAsyncWithCondition(r => r.Status == RequestStatus.Pending || r.Status == RequestStatus.Approved);
//
//            if (roomTransfers != null && roomTransfers.Any())
//            {
//                var hasChanged = false;
//                foreach (var roomBooking in roomTransfers)
//                {
//                    //If now is after reject date, reject room booking
//                    if (DateTime.Now.AddHours(GlobalParams.TimeZone) > roomBooking.RejectDate)
//                    {
//                        //If request is already approve, get student out of the room
//                        if (roomBooking.Status == RequestStatus.Approved && roomBooking.RoomId != null)
//                        {
//                            //Get student and room from room booking
//                            var student = await _repoWrapper.Student.FindByIdAsync(roomBooking.StudentId);
//                            var room = await _repoWrapper.Room.FindByIdAsync(roomBooking.RoomId.Value);
//
//                            if (student != null && room != null)
//                            {
//                                student.RoomId = null;
//                                room.CurrentNumberOfStudent--;
//                                roomBooking.Status = RequestStatus.Rejected;
//                                roomBooking.Reason = GlobalParams.DefaultAutoRejectRoomBookingReason;
//                                await _repoWrapper.Student.UpdateAsyncWithoutSave(student, student.StudentId);
//                                await _repoWrapper.Room.UpdateAsyncWithoutSave(room, room.RoomId);
//                                await _repoWrapper.RoomBooking.UpdateAsyncWithoutSave(roomBooking,
//                                    roomBooking.RoomBookingRequestFormId);
//                                hasChanged = true;
//                            }
//                        }
//                        //If request is not approved
//                        else
//                        {
//                            roomBooking.Status = RequestStatus.Rejected;
//                            roomBooking.Reason = GlobalParams.DefaultAutoRejectRoomBookingReason;
//                            await _repoWrapper.RoomBooking.UpdateAsyncWithoutSave(roomBooking,
//                                roomBooking.RoomBookingRequestFormId);
//                            hasChanged = true;
//                        }
//                    }
//                }
//
//                //If there was change, save
//                if (hasChanged)
//                {
//                    await _repoWrapper.Save();
//                }
//
//            }
//
//            return true;
//        }
    }
}