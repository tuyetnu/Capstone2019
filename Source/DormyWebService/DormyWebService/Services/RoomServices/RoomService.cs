using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.ParamEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Repositories;
using DormyWebService.Services.ParamServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.RoomViewModels.CreateRoom;
using DormyWebService.ViewModels.RoomViewModels.GetRoomTypeInfo;
using DormyWebService.ViewModels.RoomViewModels.UpdateRoom;
using Microsoft.EntityFrameworkCore.Internal;
using Sieve.Models;
using Sieve.Services;

namespace DormyWebService.Services.RoomServices
{
    public class RoomService : IRoomService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private IMapper _mapper;
        private readonly IParamService _param;
        private readonly ISieveProcessor _sieveProcessor;

        public RoomService(IRepositoryWrapper repoWrapper, IMapper mapper, IParamService param,
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

        public async Task<List<Room>> ParseRoomAsync(List<CreateRoomRequest> createRoomRequests)
        {
            List<Room> rooms = new List<Room>();
            foreach (CreateRoomRequest createRoomRequest in createRoomRequests)
            {
                Room room = CreateRoomRequest.NewRoomFromRequest(createRoomRequest);
                Param param = await _repoWrapper.Param.FindByIdAsync(createRoomRequest.RoomType);
                if (param.Name == "Standard Room")
                {
                    room.Capacity = 8;
                }
                else
                {
                    room.Capacity = 4;
                }
                room.Price = param.DecimalValue.Value;
                List<RoomTypesAndEquipmentTypes> roomTypesAndEquipmentTypes = (await _repoWrapper.RoomTypesAndEquipmentTypes.FindAllAsyncWithCondition(x => x.RoomTypeId == param.ParamId)).ToList();
               foreach(RoomTypesAndEquipmentTypes roomTypesAndEquipmentType in roomTypesAndEquipmentTypes)
                {
                    List<Equipment> equipments= (await _repoWrapper.Equipment.FindAllAsyncWithCondition(x => x.EquipmentTypeId == roomTypesAndEquipmentType.EquipmentTypeId && x.RoomId == null)).Take(roomTypesAndEquipmentType.Amount).ToList();
                    room.Equipments = equipments;
                }
                rooms.Add(room);
            }
            return rooms;
        }

        public async Task<BuildingResponse> CreateBuilding(CreateBuildingRequest requestModel)
        {
            try
            {
                Building building = CreateBuildingRequest.NewBuildingAndRoomsFromRequest(requestModel);
                building.Rooms = await ParseRoomAsync(requestModel.CreateRoomRequests);
                var result = await _repoWrapper.Building.CreateAsync(building);
                await _repoWrapper.Save();
                return new BuildingResponse()
                {
                    BuildingId = result.BuildingId
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
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
                (List<Equipment>)await _repoWrapper.Equipment.FindAllAsyncWithCondition(e => e.RoomId == room.RoomId);
            List<int> equipmentIds = null;

            if (EnumerableExtensions.Any(equipments))
            {
                equipmentIds = equipments.Select(e => e.EquipmentId).ToList();
            }

            return UpdateRoomResponse.ResponseFromRoom(room, equipmentIds);
        }

        public async Task<List<GetRoomTypeInfoResponse>> GetRoomTypeInfo()
        {
            //Get All active rooms with vacancy in database
            var rooms = (List<Room>)await _repoWrapper.Room.FindAllAsyncWithCondition(r => r.RoomStatus == RoomStatus.Active && r.CurrentNumberOfStudent < r.Capacity);

            if (rooms == null || !rooms.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: Couldn't find any room");
            }

            //Get All Room Types
            var roomTypes =
                (List<Param>)await _repoWrapper.Param.FindAllAsyncWithCondition(p =>
                   p.ParamTypeId == GlobalParams.ParamTypeRoomType);

            if (roomTypes == null || !roomTypes.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "RoomService: Couldn't find any room type");
            }

            var result = new List<GetRoomTypeInfoResponse>();

            foreach (var room in rooms)
            {
                //If result list doesn't have this room type
                if (!result.Exists(t=>t.RoomTypeId == room.RoomType && t.Gender == room.Gender))
                {
                    var roomType = roomTypes.Find(t => t.ParamId == room.RoomType);
                    result.Add(new GetRoomTypeInfoResponse()
                    {
                        RoomTypeId = room.RoomType,
                        RoomTypeName = roomType.Name,
                        RoomTypePrice = roomType.DecimalValue,
                        RoomTypeVacancy = 0 + (room.Capacity - room.CurrentNumberOfStudent),
                        Gender = room.Gender
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

        public async Task<List<Building>> GetAllBuilding()
        {
            return (await _repoWrapper.Building.FindAllAsync()).ToList();
        }

        public async Task<List<RoomsAndEquipmentTypes>> GetAllMissingEquipmentRoom()
        {
            return (await _repoWrapper.RoomsAndEquipmentTypes.FindAllAsync()).ToList();   
        }

        public async Task<Building> GetBuildingById(int buildingId)
        {
            var building = _repoWrapper.Building.GetAllIncludeRoomAndStudentById(buildingId);
            foreach (Room room in building.Rooms)
            {
                if (room.CurrentNumberOfStudent > 0)
                {
                    room.Students = await _repoWrapper.Student.FindAllAsyncWithCondition(s => s.RoomId == room.RoomId);
                }
            }
            return building;
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