using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.AccountEntities;
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

//        private async Task<ArrangeRoomResponse> ArrangeRoomForAllApprovedRequests()
//        {
//            //Get all approve request
//            var requests =(List<RoomBookingRequestForm>) await _repoWrapper.RoomBooking.FindAllAsyncWithCondition(r => r.Status == RequestStatus.Approved);
//
//            //Check if list of approved request is empty
//            if (requests == null || !requests.Any())
//            {
//                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: No request is found");
//            }
//
//            //Get list of available room
//            var availableRooms = (List<Room>)await _repoWrapper.Room.FindAllAsyncWithCondition(r => r.CurrentNumberOfStudent < r.Capacity);
//
//            //Check if there are rooms available
//            if (availableRooms == null || !availableRooms.Any())
//            {
//                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: No Room is Available");
//            }
//
//            //Sort room list sorted by available spot
//            availableRooms.Sort((x,y) => (x.Capacity - x.CurrentNumberOfStudent).CompareTo(y.Capacity - y.CurrentNumberOfStudent));
//
//            //Get all room type
//            var RoomTypeParams = _param.FindAllByParamTypeWithoutWarning(GlobalParams.ParamTypeRoomType);
//
//            var tempStudentRoomList = new List<RoomAndStudentLists>();
//
//            for (var i = 0; i < requests.Count; i++)
//            {
//                //Get student from database
//                var student = await _repoWrapper.Student.FindByIdAsync(requests[i].StudentId);
//
//                for (var j = 0; j < tempStudentRoomList.Count; j++)
//                {
//                    if (student.Gender == tempStudentRoomList[i].Gender)
//                    {
//                        
//                    }
//                }
//            }
//        }
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