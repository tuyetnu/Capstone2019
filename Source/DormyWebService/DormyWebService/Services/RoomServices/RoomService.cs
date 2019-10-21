using System;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Repositories;
using DormyWebService.Services.EquipmentServices;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.RoomViewModels;
using Microsoft.EntityFrameworkCore.Internal;

namespace DormyWebService.Services.RoomServices
{
    public class RoomService : IRoomService
    {
        private readonly int _roomTypeParamTypeId = 4;
        private readonly IRepositoryWrapper _repoWrapper;
        private IMapper _mapper;
        private IAdminService _admin;

        public RoomService(IRepositoryWrapper repoWrapper, IMapper mapper, IAdminService admin)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
            _admin = admin;
        }

        public async Task<Room> FindById(int id)
        {
            Room result;
            try
            {
                result = await _repoWrapper.Room.FindByIdAsync(id);
            }
            catch (Exception)
            {
                throw new HttpStatusCodeException(500, "RoomService: Internal Server Error Occured when finding room");
            }

            //Check if there's this user in database
            if (result == null)
            {
                throw new HttpStatusCodeException(404, "RoomService: Room is not found");
            }

            return result;
        }

        public async Task<CreateRoomResponse> CreateRoom(CreateRoomRequest requestModel)
        {
            Room room;

            //Check if room type exists
            if (await _repoWrapper.Param.FindAllAsyncWithCondition(p =>
                    p.ParamId == requestModel.RoomType && p.ParamTypeId == _roomTypeParamTypeId) == null)
            {
                throw new HttpStatusCodeException(400, "RoomService: RoomType is not valid");
            }

            try
            {
                room = await _repoWrapper.Room.CreateAsync(CreateRoomRequest.NewRoomFromRequest(requestModel));
            }
            catch (Exception)
            {
                throw new HttpStatusCodeException(500, "RoomService: Internal Server Error Occured when creating new room");
            }

            //If there are equipments
            if (requestModel.EquipmentIds.Any())
            {
                foreach (var equipmentId in requestModel.EquipmentIds)
                {
                    Equipment equipment;
                    try
                    {
                        //Find if equipment exists
                        equipment = await _repoWrapper.Equipment.FindByIdAsync(equipmentId);
                    }
                    catch (Exception)
                    {
                        throw new HttpStatusCodeException(500, "RoomService: Internal Server Error Occured when finding equipment with Id:" + equipmentId);
                    }

                    if (equipment == null)
                    {
                        throw new HttpStatusCodeException(404, "RoomService: Equipment with id " + equipmentId + " Is not found");
                    }
                    
                    //Update room id for the found equipment
                    equipment.RoomId = room.RoomId;
                    try
                    {
                        equipment = await _repoWrapper.Equipment.UpdateAsyncWithoutSave(equipment, equipment.EquipmentId);
                    }
                    catch (Exception)
                    {
                        throw new HttpStatusCodeException(500, "RoomService: Internal Server Error Occured when updating room for equipment with Id:" + equipment.EquipmentId);
                    }
                    //Save multiple records
                    _repoWrapper.Save();
                }
                
            }

            return CreateRoomResponse.ResponseFromRoom(room, requestModel.EquipmentIds);
        }
    }
}