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
using DormyWebService.ViewModels.RoomViewModels.ArrangeRoom;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.EditRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.GetRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.GetRoomBookingDetail;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.ImportRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.RejectRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.SendRoomBooking;
using Hangfire;
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
            var student = await _studentService.FindById(request.StudentId);

            //Forbid if student already has a room
            if (student.RoomId != null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Forbidden, "RoomBookingService: Student already has a room");
            }

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

            //Get max day for approving room booking
            var maxDayForApproveRoomBookingParam =
                await _repoWrapper.Param.FindByIdAsync(GlobalParams.MaxDayForApproveRoomBooking);
            if (maxDayForApproveRoomBookingParam?.Value == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Forbidden, "RoomBookingService: There are already active booking requests for this account");
            }

            //Create new room booking from request
            var result = SendRoomBookingRequest.NewEntityFromRequest(request, maxDayForApproveRoomBookingParam.Value.Value);

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

            //Update data
            roomBooking = EditRoomBookingRequest.UpdateFromRequest(roomBooking,request);

            //Save to database
            await _repoWrapper.RoomBooking.UpdateAsync(roomBooking, roomBooking.RoomBookingRequestFormId);

            return true;
        }

        public async Task<ArrangeRoomResponseStudent> ApproveRoomBookingRequest(int id)
        {
            var roomBooking = await FindById(id);

            //Check if room request
            if (roomBooking.Status != RequestStatus.Pending)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Forbidden, "RoomService: Request is not a pending request");
            }

            //Get student by id in room booking
            var student = await _repoWrapper.Student.FindByIdAsync(roomBooking.StudentId);
            if (student == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: Student not found");
            }

            //Get active room with appropriate gender sorted by ascending room vacancy
            var rooms = await _repoWrapper.Room.GetAllActiveRoomWithSpecificGenderAndRoomTypeSortedByVacancy(student.Gender, roomBooking.TargetRoomType);
            if (rooms == null || !rooms.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: Suitable Room not found");
            }

            var room = rooms[0];

            //Attach room's id to student
            student.RoomId = room.RoomId;
            student.IsHold = true;
            room.CurrentNumberOfStudent++;
            roomBooking.Status = RequestStatus.Approved;
            var maxDayForCompleteRoomBooking = await _paramService.FindById(GlobalParams.MaxDayForCompleteRoomBooking);
            if (maxDayForCompleteRoomBooking?.Value == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: MaxDayForCompleteRoomBooking not found");
            }
            var rejectDate = DateHelper.AddBusinessDays(DateTime.Now.AddHours(GlobalParams.TimeZone), maxDayForCompleteRoomBooking.Value.Value);
            roomBooking.RejectDate = new DateTime(rejectDate.Year, rejectDate.Month, rejectDate.Day, 17, 59, 0);

            await _repoWrapper.Student.UpdateAsyncWithoutSave(student, student.StudentId);
            await _repoWrapper.Room.UpdateAsyncWithoutSave(room, room.RoomId);
            await _repoWrapper.RoomBooking.UpdateAsyncWithoutSave(roomBooking, roomBooking.RoomBookingRequestFormId);
            await _repoWrapper.Save();

            return ArrangeRoomResponseStudent.ResponseFromEntity(student, room, roomBooking);
        }

        public async Task<bool> RejectRoomBookingRequest(RejectRoomBookingRequest request)
        {
            var roomBooking = await FindById(request.RoomBookingId);

            switch (roomBooking.Status)
            {
                case RequestStatus.Complete:
                case RequestStatus.Rejected:
                    return false;
                case RequestStatus.Approved:
                {
                    var student = await _studentService.FindById(roomBooking.StudentId);
                    if (student.RoomId == null)
                    {
                        return false;
                    }

                    var room = await _repoWrapper.Room.FindByIdAsync(student.RoomId.Value);
                    student.RoomId = null;
                    room.CurrentNumberOfStudent--;
                    roomBooking.RoomId = null;
                    await _repoWrapper.Student.UpdateAsyncWithoutSave(student, student.StudentId);
                    await _repoWrapper.Room.UpdateAsyncWithoutSave(room, room.RoomId);
                    break;
                }
            }

            roomBooking.LastUpdated = DateTime.Now.AddHours(GlobalParams.TimeZone);
            roomBooking.Status = RequestStatus.Rejected;
            roomBooking.Reason = request.Reason;

            await _repoWrapper.Save();

            return true;
        }

        public async Task<bool> CompleteRoomBookingRequest(int id)
        {
            var roomBooking = await FindById(id);
            switch (roomBooking.Status)
            {
                //If the request is approved, update status
                case RequestStatus.Approved:
                    roomBooking.LastUpdated = DateTime.Now.AddHours(GlobalParams.TimeZone);
                    roomBooking.Status = RequestStatus.Complete;
                    await _repoWrapper.RoomBooking.UpdateAsyncWithoutSave(roomBooking, roomBooking.RoomBookingRequestFormId);
                    //PushNotificationToFirebase pushNotificationToFirebase = new PushNotificationToFirebase();
                    //String[] diviceId = { "dheJKPHm0q4:APA91bGHc2HBrNtoXYb9kS3XcrWqy3SM8oSzGhJ6g0jHBFcA98unM9a3nCUETyPQktCgzQ2kt3PFn0jTtQ11Sy-FB0wqHDn1ao_SjO-5Cr0SaOYjrS4w3i7jBXrzLQsWenAVXW3zf-Ta" };
                    //await pushNotificationToFirebase.PushNotification(diviceId, "Dormitory", "Yêu cầu của bạn đã được Complete");
                    break;
                //If request status is not approved, return false
                default:
                    return false;
            }

            //Update images to student profile and 
            var student = await _studentService.FindById(roomBooking.StudentId);
            student.PriorityType = roomBooking.PriorityType;
            student.IsHold = false;
            if (roomBooking.IdentityCardImageUrl != null)
            {
                student.IdentityCardImageUrl = roomBooking.IdentityCardImageUrl;
            }

            if (roomBooking.PriorityImageUrl != null)
            {
                student.PriorityImageUrl = roomBooking.PriorityImageUrl;
            }
            if (roomBooking.StudentCardImageUrl != null)
            {
                student.IdentityCardImageUrl = roomBooking.StudentCardImageUrl;
            }

            await _repoWrapper.Student.UpdateAsyncWithoutSave(student, student.StudentId);

            //Create new contract
            var tempEndTime = DateTime.Now.AddHours(GlobalParams.TimeZone).AddMonths(roomBooking.Month - 1);
            var contract = new Contract()
            {
                CreatedDate = DateTime.Now.AddHours(GlobalParams.TimeZone),
                LastUpdate = DateTime.Now.AddHours(GlobalParams.TimeZone),
                StartDate = DateTime.Now.AddHours(GlobalParams.TimeZone),
                EndDate = new DateTime(tempEndTime.Year, tempEndTime.Month, DateTime.DaysInMonth(tempEndTime.Year, tempEndTime.Month), 23, 59, 59),
                Status = ContractStatus.Active,
                StudentId = roomBooking.StudentId,
            };
            _repoWrapper.Contract.CreateWithoutSave(contract);

            await _repoWrapper.Save();

            return true;
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

            var priorityType = await _paramService.FindById(roomBooking.PriorityType);

            var roomType = await _paramService.FindById(roomBooking.TargetRoomType);

            Room room = null;

            if (roomBooking.RoomId != null)
            {
                room = await _repoWrapper.Room.FindByIdAsync(roomBooking.RoomId.Value);
            }

            return GetRoomBookingDetailResponse.ResponseFromEntity(roomBooking, student, roomType, priorityType, room);
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

        //Retry up to three times if fail
        [AutomaticRetry(Attempts = 3)]
        public async Task<bool> AutoRejectRoomBooking()
        {
            var roomBookings =  (List<RoomBookingRequestForm>) await
                _repoWrapper.RoomBooking.FindAllAsyncWithCondition(r => r.Status == RequestStatus.Pending || r.Status == RequestStatus.Approved);

            if (roomBookings != null && roomBookings.Any())
            {
                var hasChanged = false;
                foreach (var roomBooking in roomBookings)
                {
                    //If now is after reject date, reject room booking
                    if (DateTime.Now.AddHours(GlobalParams.TimeZone) > roomBooking.RejectDate)
                    {
                        //If request is already approve, get student out of the room
                        if (roomBooking.Status == RequestStatus.Approved && roomBooking.RoomId != null)
                        {
                            //Get student and room from room booking
                            var student = await _repoWrapper.Student.FindByIdAsync(roomBooking.StudentId);
                            var room = await _repoWrapper.Room.FindByIdAsync(roomBooking.RoomId.Value);

                            if (student!=null && room !=null)
                            {
                                student.RoomId = null;
                                room.CurrentNumberOfStudent--;
                                roomBooking.Status = RequestStatus.Rejected;
                                roomBooking.LastUpdated = DateTime.Now.AddHours(GlobalParams.TimeZone);
                                roomBooking.Reason = GlobalParams.DefaultAutoRejectRoomBookingReason;
                                await _repoWrapper.Student.UpdateAsyncWithoutSave(student, student.StudentId);
                                await _repoWrapper.Room.UpdateAsyncWithoutSave(room, room.RoomId);
                                await _repoWrapper.RoomBooking.UpdateAsyncWithoutSave(roomBooking,
                                    roomBooking.RoomBookingRequestFormId);
                                hasChanged = true;
                            }
                        }
                        //If request is not approved
                        else
                        {
                            roomBooking.Status = RequestStatus.Rejected;
                            roomBooking.LastUpdated = DateTime.Now.AddHours(GlobalParams.TimeZone);
                            roomBooking.Reason = GlobalParams.DefaultAutoRejectRoomBookingReason;
                            await _repoWrapper.RoomBooking.UpdateAsyncWithoutSave(roomBooking,
                                roomBooking.RoomBookingRequestFormId);
                            hasChanged = true;
                        }
                    }
                }

                //If there was change, save
                if (hasChanged)
                {
                    await _repoWrapper.Save();
                }
                
            }

            return true;
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

        public async Task<ArrangeRoomResponse> ImportRoomBookingRequests(List<ImportRoomBookingRequest> requests)
        {
            //Get all students from email in request
            var students =
                (List<Student>)await _repoWrapper.Student.FindAllAsyncWithCondition(s =>
                   requests.Exists(r => r.Email == s.Email));
            if (students == null || !students.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: No student is found");
            }

            if (students.Count != requests.Count)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "RoomService: Some Student does not exist in database");
            }

            if (students.Find(s => s.RoomId != null) != null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "RoomService: Some Student already has a room");
            }

            //Get max day to create new room booking
            var maxDayForApproveRoomBooking = await _paramService.FindById(GlobalParams.MaxDayForApproveRoomBooking);
            if (maxDayForApproveRoomBooking?.Value == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: maxDayForApproveRoomBooking not found");
            }

            // Create List of objects that contains a Student, created RoomBooking from that student
            var importStudentAndRequests = new List<ImportStudentAndRequest>();
            foreach (var request in requests)
            {
                var student = students.Find(s => s.Email == request.Email);
                var roomBooking = ImportRoomBookingRequest.EntityFromRequest(request, student.StudentId, maxDayForApproveRoomBooking.Value.Value);
                importStudentAndRequests.Add(new ImportStudentAndRequest() { Student = student, RoomBooking = roomBooking });
            }

            //Get list of available room
            var availableRooms = (List<Room>)await _repoWrapper.Room.FindAllAsyncWithCondition(r => r.CurrentNumberOfStudent < r.Capacity);

            //Check if there are rooms available
            if (availableRooms == null || !availableRooms.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: No Room is Available");
            }

            //Sort room list sorted by available spot
            availableRooms.Sort((x, y) => (x.Capacity - x.CurrentNumberOfStudent).CompareTo(y.Capacity - y.CurrentNumberOfStudent));

            var arrangedStudents = new List<ImportStudentAndRequest>();
            var unArrangedStudents = new List<Student>();
            var arrangedRooms = new List<Room>();

            //Go through every object
            for (var i = 0; i < importStudentAndRequests.Count; i++)
            {
                //Get the student from object
                var student = importStudentAndRequests[i].Student;
                var roomBooking = importStudentAndRequests[i].RoomBooking;

                //if already ran out of available room, add remaining students to unArrangedStudents
                if (availableRooms.Count <= 0)
                {
                    unArrangedStudents.Add(student);
                }
                //If there's still room, try to arrange room for current student
                else
                {
                    //Go through every available rooms, which has been previously sorted
                    for (var j = 0; j < availableRooms.Count; j++)
                    {
                        var currentRoom = availableRooms[j];
                        //If there's a room that satisfies student's requirements, add student to that room
                        if (roomBooking.TargetRoomType == currentRoom.RoomType && student.Gender == currentRoom.Gender)
                        {
                            //add student to room
                            student.RoomId = currentRoom.RoomId;
                            if (!arrangedRooms.Contains(currentRoom))
                            {
                                arrangedRooms.Add(currentRoom);
                            }
                            //Increase current student number of room
                            currentRoom.CurrentNumberOfStudent++;
                            //add student to arrangedStudentList to save to database 
                            arrangedStudents.Add(new ImportStudentAndRequest() { Student = student, RoomBooking = roomBooking });
                            //add room Id into room booking request;
                            roomBooking.RoomId = currentRoom.RoomId;
                            //Pend creating roomBooking
                            _repoWrapper.RoomBooking.CreateWithoutSave(roomBooking);
                            //Add arranged student to list of pending database update
                            await _repoWrapper.Student.UpdateAsyncWithoutSave(student, student.StudentId);
                            //If room is full after adding the student
                            if (currentRoom.Capacity == currentRoom.CurrentNumberOfStudent)
                            {
                                //Remove full current room from available room list so we won't have to check this room again
                                availableRooms.RemoveAt(j);
                                //Add room to pending changes to database
                                await _repoWrapper.Room.UpdateAsyncWithoutSave(currentRoom, currentRoom.RoomId);
                            }
                            //Break loop and move on to next request and go through list of available room again
                            break;
                        }

                        //if at the end of available room list and hasn't found a suitable room yet, add current student to unArrangedStudents list
                        if (j == availableRooms.Count - 1)
                        {
                            //Add current student to unArrangedStudentList
                            unArrangedStudents.Add(student);
                        }
                    }
                }
            }

            //Save all students, rooms and requests at once, roll back everything if something went wrong
            await _repoWrapper.Save();

            //If save is successful, preparing response message
            return ArrangeRoomResponse.ArrangeRoomListFromEntities(arrangedStudents, unArrangedStudents, arrangedRooms);
        }
    }
}