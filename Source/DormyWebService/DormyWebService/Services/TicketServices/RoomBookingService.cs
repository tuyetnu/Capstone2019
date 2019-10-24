using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Repositories;
using DormyWebService.Services.ParamServices;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.EditRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.GetRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.ResolveRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.SendRoomBooking;
using Sieve.Models;
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
            if (!await _paramService.IsOfParamType(request.PriorityType, GlobalParams.ParamTypeStudentPriorityType))
            { 
                throw new HttpStatusCodeException(400, "RoomBookingService: PriorityType is invalid");
            }

            if (request.Month <= 0)
            {
                throw new HttpStatusCodeException(400, "RoomBookingService: Month is invalid");
            }

            if (!await _paramService.IsOfParamType(request.TargetRoomType, GlobalParams.ParamTypeRoomType))
            {
                throw new HttpStatusCodeException(400, "RoomBookingService: RoomType is invalid");
            }

            var student = await _studentService.FindById(request.StudentId);

            //Check for active requests
            var bookings = (List<RoomBookingRequestForm>)
                await _repoWrapper.RoomBooking.FindAllAsyncWithCondition(r => r.StudentId == request.StudentId);

            if (bookings != null)
            {
                if (bookings.Exists(b => b.Status == RequestStatus.Pending || b.Status == RequestStatus.Approved))
                {
                    throw new HttpStatusCodeException(403, "RoomBookingService: There are already active booking requests for this account");
                }
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

            return new SendRoomBookingResponse()
            {
                RoomBookingRequestFormId = result.RoomBookingRequestFormId
            };
        }

        public Task<bool> EditRoomRequest(EditRoomBookingRequest request)
        {
            _studentService.FindById(request.StudentId);

            return null;
        }

        public async Task<ResolveRoomBookingResponse> ResolveRequest(ResolveRoomBookingRequest request)
        {
            //Check if this staff exist
            var staff = await _userService.FindById(request.StaffId);
            if (staff.Role != Role.Staff)
            {
                throw new HttpStatusCodeException(400, "RoomBookingService: Provide StaffId is not a staff");
            }

            //Check if Room Booking Request Exists
            var roomBooking = await FindById(request.RoomBookingRequestFormId);

            //Update information into DataSet
            roomBooking = _mapper.Map(request, roomBooking);
            //Update Last Updated Date
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

        public async Task<List<GetRoomBookingResponse>> AdvancedGetRoomRequest(string sorts, string filters, int? page, int? pageSize)
        {
            //Build model for SieveProcessor
            var sieveModel = new SieveModel()
            {
                PageSize = pageSize,
                Sorts = sorts,
                Page = page,
                Filters = filters
            };

            ICollection<RoomBookingRequestForm> roomBookings;

            try
            {
                //Get all RoomBookings
                roomBookings = await _repoWrapper.RoomBooking.FindAllAsync();
            }
            catch (Exception)
            {
                throw new HttpStatusCodeException(500, "RoomBookingService: Internal Server Error Occured Searching for Room Booking Request");
            }

            if (roomBookings == null || roomBookings.Any() == false)
            {
                throw new HttpStatusCodeException(404, "RoomBookingService: No Request is found");
            }

            //Apply filter, sort, pagination
            var result = _sieveProcessor.Apply(sieveModel, roomBookings.AsQueryable()).ToList();

            return result.Select(r=>_mapper.Map<GetRoomBookingResponse>(r)).ToList(); 
        }

        public async Task<bool> DeleteRoomBooking(int id)
        {
            try
            {
                if (await _repoWrapper.RoomBooking.DeleteAsync(await FindById(id)) > 0)
                {
                    return true;
                }
            }
            catch (DbException)
            {
                throw new HttpStatusCodeException(500, "RoomBookingService: Internal Server Error Occured when deleting Room Booking Request");
            }

            return false;
        }
    }
}