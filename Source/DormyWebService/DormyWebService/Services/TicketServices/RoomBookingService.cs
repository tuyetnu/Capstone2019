using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Repositories;
using DormyWebService.Services.ParamServices;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.ResolveRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.SendRoomBooking;
using Sieve.Services;

namespace DormyWebService.Services.TicketServices
{
    public class RoomBookingService : IRoomBookingService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IMapper _mapper;
        private ISieveProcessor _sieveProcessor;
        private readonly IStudentService _studentService;
        private readonly IParamService _paramService;
        private readonly IUserService _userService;

        public RoomBookingService(IRepositoryWrapper repoWrapper, IMapper mapper, ISieveProcessor sieveProcessor, IStudentService studentService, IParamService paramService, IUserService userService)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
            _studentService = studentService;
            _paramService = paramService;
            _userService = userService;
        }

        public async Task<RoomBookingRequestForm> FindById(int id)
        {
            RoomBookingRequestForm result;
            try
            {
                result = await _repoWrapper.RoomBooking.FindByIdAsync(id);
            }
            catch (Exception)
            {
                throw new HttpStatusCodeException(500, "RoomBookingService: Internal Server Error Occured when finding Room Booking");
            }

            if (result == null)
            {
                throw new HttpStatusCodeException(404, "RoomBookingService: Room Booking is not found");
            }

            return result;
        }

        public async Task<SendRoomBookingResponse> SendRequest(SendRoomBookingRequest request)
        {
            if (request.Month <= 0)
            {
                throw new HttpStatusCodeException(400, "RoomBookingService: Month is invalid");
            }

            if (!await _paramService.IsOfParamType(request.TargetRoomType, GlobalParams.ParamTypeRoomType))
            {
                throw new HttpStatusCodeException(400, "RoomBookingService: RoomType is invalid");
            }

            var student = await _studentService.FindById(request.StudentId);

            var bookings = (List<RoomBookingRequestForm>)
                await _repoWrapper.RoomBooking.FindAllAsyncWithCondition(r => r.StudentId == request.StudentId);

            if (bookings.Exists(b => b.Status == RequestStatus.Pending || b.Status == RequestStatus.Approved))
            {
                throw new HttpStatusCodeException(403, "RoomBookingService: There are already active booking requests for this account");
            }

            var result = SendRoomBookingRequest.NewEntityFromReQuest(request);

            try
            {
                result = await _repoWrapper.RoomBooking.CreateAsync(result);
            }
            catch (Exception)
            {
                throw new HttpStatusCodeException(500, "RoomBookingService: DbException happened when creating new request");
            }

            return _mapper.Map<SendRoomBookingResponse>(result);
        }

        public async Task<ResolveRoomBookingResponse> ResolveRequest(ResolveRoomBookingRequest request)
        {
            //Check if this staff exist
            var staff = await _userService.FindById(request.StaffId);

            if (staff.Role != Role.Staff)
            {
                throw new HttpStatusCodeException(400, "RoomBookingService: Provide StaffId is not a staff");
            }

            var roomBooking = await FindById(request.RoomBookingRequestFormId);

            roomBooking = _mapper.Map(request, roomBooking);
            roomBooking.LastUpdated = DateTime.Now;

            try
            {
                roomBooking =
                    await _repoWrapper.RoomBooking.UpdateAsync(roomBooking, roomBooking.RoomBookingRequestFormId);
            }
            catch (Exception)
            {
                throw new HttpStatusCodeException(500, "RoomBookingService: DbException happened when updating request");
            }

            return _mapper.Map<ResolveRoomBookingResponse>(roomBooking);
        }
    }
}