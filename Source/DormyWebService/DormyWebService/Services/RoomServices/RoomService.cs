using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.ContractEntities;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.ParamEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Repositories;
using DormyWebService.Services.EquipmentServices;
using DormyWebService.Services.ParamServices;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.RoomViewModels;
using DormyWebService.ViewModels.RoomViewModels.ArrangeRoom;
using DormyWebService.ViewModels.RoomViewModels.CreateRoom;
using DormyWebService.ViewModels.RoomViewModels.GetRoomTypeInfo;
using DormyWebService.ViewModels.RoomViewModels.UpdateRoom;
using Microsoft.EntityFrameworkCore.Internal;
using Sieve.Models;
using Sieve.Services;
using Enumerable = System.Linq.Enumerable;

namespace DormyWebService.Services.RoomServices
{
    public class RoomService : IRoomService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private IMapper _mapper;
        private readonly IParamService _param;
        private readonly ISieveProcessor _sieveProcessor;

        public RoomService(IRepositoryWrapper repoWrapper, IMapper mapper, IAdminService admin, IParamService param,
            ISieveProcessor sieveProcessor)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
            _param = param;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<Room> FindById(int id)
        {
            var result = await _repoWrapper.Room.FindByIdAsync(id);

            //Check if there's this user in database
            if (result == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: Room is not found");
            }

            return result;
        }

        public async Task<CreateRoomResponse> CreateRoom(CreateRoomRequest requestModel)
        {
            //Check if room type exists
            if (!await _param.IsOfParamType(requestModel.RoomType, GlobalParams.ParamTypeRoomType))
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: RoomType is not valid");
            }

            var room = await _repoWrapper.Room.CreateAsync(CreateRoomRequest.NewRoomFromRequest(requestModel));

            //If there are equipments
            if (EnumerableExtensions.Any(requestModel.EquipmentIds))
            {
                foreach (var equipmentId in requestModel.EquipmentIds)
                {
                    //Find if equipment exists
                    var equipment = await _repoWrapper.Equipment.FindByIdAsync(equipmentId);
                    if (equipment == null)
                    {
                        throw new HttpStatusCodeException(HttpStatusCode.NotFound,
                            "RoomService: Equipment with id " + equipmentId + " Is not found");
                    }

                    //Update room id for the found equipment
                    if (equipment.RoomId != room.RoomId)
                    {
                        equipment.RoomId = room.RoomId;
                        await _repoWrapper.Equipment.UpdateAsyncWithoutSave(equipment,
                            equipment.EquipmentId);
                    }
                }

                //Save multiple records
                await _repoWrapper.Save();
            }

            return new CreateRoomResponse()
            {
                RoomId = room.RoomId
            };
        }

        public async Task<List<Room>> AdvancedGetRooms(string sorts, string filters, int? page, int? pageSize)
        {
            var sieveModel = new SieveModel()
            {
                PageSize = pageSize,
                Sorts = sorts,
                Page = page,
                Filters = filters
            };

            var rooms = await _repoWrapper.Room.FindAllAsync();

            if (rooms == null || rooms.Any() == false)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: No room is found");
            }

            var result = _sieveProcessor.Apply(sieveModel, rooms.AsQueryable()).ToList();
            return result;
        }

        public async Task<UpdateRoomResponse> UpdateRoom(UpdateRoomRequest requestModel)
        {
            //Find and check if room exists in database
            var room = await FindById(requestModel.RoomId);

            //Check if room type exists
            if (!await _param.IsOfParamType(requestModel.RoomType, GlobalParams.ParamTypeRoomType))
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: RoomType is not valid");
            }

            //Update room with new information
            room = UpdateRoomRequest.UpdateToRoom(room, requestModel);


            //Update to database
            room = await _repoWrapper.Room.UpdateAsync(room, room.RoomId);

            var equipments =
                (List<Equipment>) await _repoWrapper.Equipment.FindAllAsyncWithCondition(e => e.RoomId == room.RoomId);
            List<int> equipmentIds = null;

            if (EnumerableExtensions.Any(equipments))
            {
                equipmentIds = equipments.Select(e => e.EquipmentId).ToList();
            }

            return UpdateRoomResponse.ResponseFromRoom(room, equipmentIds);
        }

        /// <summary>
        /// arrange room for one request, and return result without saving to database
        /// </summary>
        /// <param name="requestId">RoomBooking id</param>
        /// <returns></returns>
        public async Task<ArrangeRoomResponseStudent> ArrangeOneApprovedRequest(int requestId)
        {
            //Get room booking from id
            var roomBooking = await _repoWrapper.RoomBooking.FindByIdAsync(requestId);

            if (roomBooking == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: Room booking not found");
            }

            //Check if room request
            if (roomBooking.Status != RequestStatus.Approved)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Forbidden, "RoomService: Request is not approved yet");
            }

            //Get student by id in room booking
            var student = await _repoWrapper.Student.FindByIdAsync(roomBooking.StudentId);
            if (student == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: Student not found");
            }

            //Get active room sorted by ascending room vacancy
            var rooms = await _repoWrapper.Room.GetAllActiveRoomSortedByVacancy();
            if (rooms == null || !rooms.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: Room not found");
            }

            return ArrangeRoomResponseStudent.ResponseFromEntity(student, rooms[0], roomBooking);
        }

        public async Task<bool> SaveArrangeOneApprovedRequest(ArrangeRoomResponseStudent request)
        {
            //Get student by id in request
            var student = await _repoWrapper.Student.FindByIdAsync(request.StudentId);
            if (student == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: Student not found");
            }

            var room = await _repoWrapper.Room.FindByIdAsync(request.RoomId);
            if (room == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: Room not found");
            }

            var roomBooking = await _repoWrapper.RoomBooking.FindByIdAsync(request.RoomBookingId);
            if (roomBooking == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: Room Booking not found");
            }

            student.RoomId = request.RoomId;
            student.IdentityCardImageUrl = roomBooking.IdentityCardImageUrl;
            student.PriorityImageUrl = roomBooking.IdentityCardImageUrl;
            student.StudentCardImageUrl = roomBooking.StudentCardImageUrl;
            student.PriorityType = roomBooking.PriorityType;
            await _repoWrapper.Student.UpdateAsyncWithoutSave(student, student.StudentId);
            room.CurrentNumberOfStudent++;
            await _repoWrapper.Room.UpdateAsyncWithoutSave(room, room.RoomId);
            roomBooking.Status = RequestStatus.Complete;
            roomBooking.RoomId = room.RoomId;
            await _repoWrapper.RoomBooking.UpdateAsyncWithoutSave(roomBooking, roomBooking.RoomBookingRequestFormId);

            //Create new contract
            var tempEndTime = DateTime.Now.AddHours(GlobalParams.TimeZone).AddMonths(roomBooking.Month - 1);
            var contract = new Contract()
            {
                CreatedDate = DateTime.Now.AddHours(GlobalParams.TimeZone),
                LastUpdate = DateTime.Now.AddHours(GlobalParams.TimeZone),
                StartDate = DateTime.Now.AddHours(GlobalParams.TimeZone),
                EndDate = new DateTime(tempEndTime.Year, tempEndTime.Month, DateTime.DaysInMonth(tempEndTime.Year, tempEndTime.Month), 23,59,59),
                Status = ContractStatus.Active,
                StudentId = student.StudentId,
            };
            _repoWrapper.Contract.CreateAsyncWithoutSave(contract);

            await _repoWrapper.Save();

            return true;
        }

        public async Task<ArrangeRoomResponse> ArrangeRoomForAllApprovedRequests()
        {
            //Get all approve request
            var requests =(List<RoomBookingRequestForm>) await _repoWrapper.RoomBooking.FindAllAsyncWithCondition(r => r.Status == RequestStatus.Approved);

            //Check if list of approved request is empty
            if (requests == null || !requests.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: No approved request is found");
            }

            //Get list of available room
            var availableRooms = (List<Room>)await _repoWrapper.Room.FindAllAsyncWithCondition(r => r.CurrentNumberOfStudent < r.Capacity);

            //Check if there are rooms available
            if (availableRooms == null || !availableRooms.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: No Room is Available");
            }

            //Sort requests by CreatedDate
            requests.Sort((x, y) => DateTime.Compare(x.CreatedDate, y.CreatedDate));

            //Sort room list sorted by available spot
            availableRooms.Sort((x,y) => (x.Capacity - x.CurrentNumberOfStudent).CompareTo(y.Capacity - y.CurrentNumberOfStudent));

            var arrangedStudents = new List<Student>();
            var unArrangedStudents = new List<Student>();
            var arrangedRooms = new List<Room>();

            //Go through every requests
            for (var i = 0; i < requests.Count; i++)
            {
                //Get the student from database
                var student = await _repoWrapper.Student.FindByIdAsync(requests[i].StudentId);

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
                        if (requests[i].TargetRoomType == currentRoom.RoomType && student.Gender == currentRoom.Gender)
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
                            arrangedStudents.Add(student);
                            //add room Id into request;
                            requests[i].RoomId = currentRoom.RoomId;
                            //Pend request update
                            await _repoWrapper.RoomBooking.UpdateAsyncWithoutSave(requests[i],
                                requests[i].RoomBookingRequestFormId);
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
            return ArrangeRoomResponse.ArrangeRoomListFromEntities(arrangedStudents,unArrangedStudents,arrangedRooms);
        }

        public async Task<List<GetRoomTypeInfoResponse>> GetRoomTypeInfo()
        {
            //Get All active rooms with vacancy in database
            var rooms = (List<Room>) await _repoWrapper.Room.FindAllAsyncWithCondition(r => r.RoomStatus == RoomStatus.Active && r.CurrentNumberOfStudent < r.Capacity);

            if (rooms == null || !rooms.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: Couldn't find any room");
            }

            //Get All Room Types
            var roomTypes =
                (List<Param>) await _repoWrapper.Param.FindAllAsyncWithCondition(p =>
                    p.ParamTypeId == GlobalParams.ParamTypeRoomType);

            if (roomTypes == null || !roomTypes.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: Couldn't find any room type");
            }

            var result = new List<GetRoomTypeInfoResponse>();

            foreach (var room in rooms)
            {
                //If result list doesn't have this room type
                if (!result.Exists(t=>t.RoomTypeId == room.RoomType))
                {
                    var roomType = roomTypes.Find(t => t.ParamId == room.RoomType);
                    result.Add(new GetRoomTypeInfoResponse()
                    {
                        RoomTypeId = room.RoomType,
                        RoomTypeName = roomType.Name,
                        RoomTypePrice = roomType.DecimalValue,
                        RoomTypeVacancy = 0 + (room.Capacity - room.CurrentNumberOfStudent)
                    });
                }
                else
                {
                    var resultRoomType = result.Find(t => t.RoomTypeId == room.RoomType);
                    resultRoomType.RoomTypeVacancy += (room.Capacity - room.CurrentNumberOfStudent);
                }
            }

            return result;
        }
//
//        private List<Room> SplitRoomByGender(List<Room> src, bool gender)
//        {
//            return src.Where(room => room.Gender == gender).ToList();
//        }

//        private List<Student> ArrangRoom(List<Room> rooms, List<Student> students)
//        {
//            //Go through each room
//            foreach (var room in rooms)
//            {
//                if
//            }
//        }
    }
}