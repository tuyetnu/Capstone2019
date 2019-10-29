using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.ContractEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Repositories;
using DormyWebService.Services.ParamServices;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.EditRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.GetRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.GetRoomBookingDetail;
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
        private readonly ISieveProcessor _sieveProcessor;
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
            var result = await _repoWrapper.RoomBooking.FindByIdAsync(id);

            if (result == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomBookingService: Room Booking is not found");
            }

            return result;
        }

        public async Task<SendRoomBookingResponse> SendRequest(SendRoomBookingRequest request)
        {
            //Check request
            var checkResult =
                await Check_PriorityType_Month_TargetRoomType(request.PriorityType, request.Month,
                    request.TargetRoomType);
            if (checkResult.Code != HttpStatusCode.OK)
            {
                throw new HttpStatusCodeException(checkResult.Code, checkResult.Message);
            }

            //Find student in database
            await _studentService.FindById(request.StudentId);

            //Check for active requests
            var bookings = (List<RoomBookingRequestForm>)
                await _repoWrapper.RoomBooking.FindAllAsyncWithCondition(r => r.StudentId == request.StudentId);

            if (bookings != null)
            {
                if (bookings.Exists(b => b.Status == RequestStatus.Pending || b.Status == RequestStatus.Approved))
                {
                    throw new HttpStatusCodeException(HttpStatusCode.Forbidden, "RoomBookingService: There are already active booking requests for this account");
                }
            }

            //Create new room booking from request
            var result = SendRoomBookingRequest.NewEntityFromRequest(request);

            //Create in database
            result = await _repoWrapper.RoomBooking.CreateAsync(result);

            return new SendRoomBookingResponse()
            {
                RoomBookingRequestFormId = result.RoomBookingRequestFormId
            };
        }

        public async Task<bool> EditRoomRequest(EditRoomBookingRequest request)
        {
            //Check request
            var checkResult =
                await Check_PriorityType_Month_TargetRoomType(request.PriorityType, request.Month,
                    request.TargetRoomType);
            if (checkResult.Code != HttpStatusCode.OK)
            {
                throw new HttpStatusCodeException(checkResult.Code, checkResult.Message);
            }

            //Find Room Booking by Id
            var roomBooking = await FindById(request.RoomBookingRequestFormId);

            //Check if status is valid
            if (roomBooking.Status == RequestStatus.Approved || roomBooking.Status == RequestStatus.Complete)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Forbidden, "RoomBookingService: Can't not edit Approved and Completed Room Booking Requests'");
            }

            //Check if StudentId matches
            if (roomBooking.StudentId != request.StudentId)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Forbidden, "RoomBookingService: this student is not permitted to change this request");
            }

            //Update data
            roomBooking = EditRoomBookingRequest.UpdateFromRequest(roomBooking,request);

            //Save to database
            roomBooking = await _repoWrapper.RoomBooking.UpdateAsync(roomBooking, roomBooking.RoomBookingRequestFormId);

            return true;
        }

        public async Task<ResolveRoomBookingResponse> ResolveRequest(ResolveRoomBookingRequest request)
        {
            //Check if this staff exist
            var staff = await _repoWrapper.Staff.FindByIdAsync(request.StaffId);

            if (staff == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "RoomBookingService: Provide StaffId is not a staff");
            }

            //Check if Room Booking Request Exists
            var roomBooking = await FindById(request.RoomBookingRequestFormId);

            if (roomBooking.Status == request.Status)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "RoomBookingService: This request already has this status");
            }

            if (roomBooking.Status == RequestStatus.Complete)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "RoomBookingService: This request is already completed, can't change it to anything else''");
            }

            //Update information into DataSet
            roomBooking = _mapper.Map(request, roomBooking);

            //Update Last Updated Date
            roomBooking.LastUpdated = DateTime.Now.AddHours(7);

            //Update to database
            roomBooking =
                    await _repoWrapper.RoomBooking.UpdateAsyncWithoutSave(roomBooking, roomBooking.RoomBookingRequestFormId);

            //If request is changed to complete
            if (request.Status == RequestStatus.Complete)
            {
                //Get contract that have the same studentId
                System.Diagnostics.Debug.WriteLine("roomBooking.StudentId: " + roomBooking.StudentId);
                var tempContractList = (List<Contract>)await
                    _repoWrapper.Contract.FindAllAsyncWithCondition(c => c.StudentId == roomBooking.StudentId);

                //Create new contract if this student doesn't have any active contract
                if (tempContractList != null && !tempContractList.Exists(c=>c.Status == ContractStatus.Active))
                {
                    var contract = new Contract()
                    {
                        StudentId = roomBooking.StudentId,
                        CreatedDate = DateTime.Now.AddHours(7),
                        LastUpdate = DateTime.Now.AddHours(7),
                        //Set to Start of next month
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month +1, 1,
                            0, 0, 0, DateTime.Now.Kind),
                        Status = ContractStatus.NotActiveYet,
                    };
                    //Set end to StartDate + RoomBooking.Month
                    contract.EndDate = contract.StartDate.AddMonths(roomBooking.Month);

                    _repoWrapper.Contract.CreateAsyncWithoutSave(contract);
                }
            }

            //Save to database at once
            await _repoWrapper.Save();

            //Return mapped response
            return _mapper.Map<ResolveRoomBookingResponse>(roomBooking);
        }

        public async Task<AdvancedGetRoomBookingResponse> AdvancedGetRoomRequest(string sorts, string filters, int? page, int? pageSize)
        {
            //Build model for SieveProcessor
            var sieveModel = new SieveModel()
            {
                PageSize = pageSize,
                Sorts = sorts,
                Page = page,
                Filters = filters
            };

            //Get all RoomBookings
            var roomBookings = (List<RoomBookingRequestForm>) await _repoWrapper.RoomBooking.FindAllAsync();

            if (roomBookings == null || roomBookings.Any() == false)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomBookingService: No Request is found");
            }

            var resultResponses = new List<GetRoomBookingResponse>();

            foreach (var form in roomBookings)
            {
                var student = await _repoWrapper.Student.FindByIdAsync(form.StudentId);
                var roomType = await _repoWrapper.Param.FindByIdAsync(form.TargetRoomType);

                Room room = null;

                if (form.RoomId != null)
                {
                    room = await _repoWrapper.Room.FindByIdAsync(form.RoomId.Value);
                }
                
                resultResponses.Add(GetRoomBookingResponse.ResponseFromEntity(form, student, roomType, room));
            }

            //Apply filter, sort
            var result = _sieveProcessor.Apply(sieveModel, resultResponses.AsQueryable(), applyPagination: false).ToList();

            var response = new AdvancedGetRoomBookingResponse
            {
                CurrentPage = page ?? 1,
                TotalPage = (int) Math.Ceiling((double) result.Count / pageSize ?? 1),
                //Apply pagination
                ResultList = _sieveProcessor
                    .Apply(sieveModel, result.AsQueryable(), applyFiltering: false, applySorting: false).ToList()
            };

            //Return List of result
            return response; 
        }

        public async Task<GetRoomBookingDetailResponse> GetRoomBookingDetail(int id)
        {
            var roomBooking = await FindById(id);

            var student = await _studentService.FindById(roomBooking.StudentId);

            var priorityType = await _paramService.FindById(student.PriorityType);

            var roomType = await _paramService.FindById(roomBooking.TargetRoomType);

            Room room = null;

            if (roomBooking.RoomId != null)
            {
                room = await _repoWrapper.Room.FindByIdAsync(roomBooking.RoomId.Value);
            }

            return GetRoomBookingDetailResponse.ResponseFromEntity(roomBooking, student, roomType, priorityType, room);
        }

        public async Task<bool> DeleteRoomBooking(int id)
        {
            return await _repoWrapper.RoomBooking.DeleteAsync(await FindById(id)) > 0;
        }

        public async Task<bool> StudentHasRoomRequestWithStatus(int studentId, List<string> statuses)
        {
            //Find if student exists
            var student = await _repoWrapper.Student.FindByIdAsync(studentId);
            if (student == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomBookingService: Student not found");
            }

            List<RoomBookingRequestForm> roomBooking;

            if (!statuses.Any())
            {
                roomBooking =
                    (List<RoomBookingRequestForm>) await _repoWrapper.RoomBooking.FindAllAsyncWithCondition(r =>
                        r.StudentId == studentId);
            }

            else
            {
                roomBooking =
                    (List<RoomBookingRequestForm>)await _repoWrapper.RoomBooking.FindAllAsyncWithCondition(r =>
                        r.StudentId == studentId && statuses.Contains(r.Status));
            }

            return roomBooking.Any();
        }

        //Used to check request for Send and edit room booking
        private async Task<HttpCodeReturn> Check_PriorityType_Month_TargetRoomType(int priorityType, int month, int targetRoomType)
        {
            //Check if PriorityType is valid
            if (!await _paramService.IsOfParamType(priorityType, GlobalParams.ParamTypeStudentPriorityType))
            {
                return new HttpCodeReturn(HttpStatusCode.BadRequest, "RoomBookingService: PriorityType is Invalid");
            }

            //Check if Month is valid
            if (month <= 0)
            {
                return new HttpCodeReturn(HttpStatusCode.BadRequest, "RoomBookingService: Month is invalid");
            }

            //Check if TargetRoomType is valid
            if (!await _paramService.IsOfParamType(targetRoomType, GlobalParams.ParamTypeRoomType))
            {
                return new HttpCodeReturn(HttpStatusCode.BadRequest, "RoomBookingService: RoomType is invalid");
            }

            //return ok
            return new HttpCodeReturn(HttpStatusCode.OK);
        }
    }
}