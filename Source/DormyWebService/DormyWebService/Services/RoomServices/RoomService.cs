using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.ParamEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Repositories;
using DormyWebService.Services.EquipmentServices;
using DormyWebService.Services.ParamServices;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.RoomViewModels;
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

                    if (equipment.RoomId == room.RoomId) continue;
                    //Update room id for the found equipment
                    equipment.RoomId = room.RoomId;
                    equipment = await _repoWrapper.Equipment.UpdateAsyncWithoutSave(equipment, equipment.EquipmentId);
                }

                //Save multiple records
                await _repoWrapper.Save();
            }

            return CreateRoomResponse.ResponseFromRoom(room, requestModel.EquipmentIds);
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
    }
}