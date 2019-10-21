using System;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Repositories;
using DormyWebService.Services.ParamServices;
using DormyWebService.Services.RoomServices;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.EquipmentViewModels.CreateEquipment;
using DormyWebService.ViewModels.EquipmentViewModels.UpdateEquipment;
using DormyWebService.ViewModels.NewsViewModels.UpdateNews;

namespace DormyWebService.Services.EquipmentServices
{
    public class EquipmentService : IEquipmentService
    {
        private IRepositoryWrapper _repoWrapper;
        private IMapper _mapper;
        private IAdminService _admin;
        private IRoomService _room;
        private IParamService _param;

        public EquipmentService(IRepositoryWrapper repoWrapper, IMapper mapper, IAdminService admin, IRoomService room, IParamService param)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
            _admin = admin;
            _room = room;
            _param = param;
        }

        public async Task<Equipment> FindById(int id)
        {
            Equipment equipment;
            try
            {
                equipment = await _repoWrapper.Equipment.FindByIdAsync(id);
            }
            catch (Exception)
            {
                throw new HttpStatusCodeException(500, "Internal Server Error Occured when finding Equipment");
            }

            //Check if there's this user in database
            if (equipment == null)
            {
                throw new HttpStatusCodeException(404, "Equipment is not found");
            }

            return equipment;
        }

        public async Task<CreateEquipmentResponse> CreateEquipment(CreateEquipmentRequest requestModel)
        {
            Room room = null;
            //If there's a room, check if the room exists 
            if (requestModel.RoomId != null)
            {
                room = await _room.FindById(int.Parse(requestModel.RoomId));
            }

            Equipment equipment;
            try
            {   
                equipment =
                    await _repoWrapper.Equipment.CreateAsync(
                        CreateEquipmentRequest.NewEquipmentFromRequest(requestModel, room));
            }
            catch (Exception)
            {
                throw new HttpStatusCodeException(500, "EquipmentService: Internal Server Error Occured when creating new equipment");
            }

            return CreateEquipmentResponse.CreateFromEquipment(equipment, room);
        }

        public async Task<UpdateEquipmentResponse> UpdateEquipment(UpdateEquipmentRequest requestModel)
        {
            Room room = null;

            //If there's a room, check if the room exists 
            if (requestModel.RoomId != null)
            {
                room = await _room.FindById(int.Parse(requestModel.RoomId));
            }

            //Get equipment from database
            var equipment = await FindById(requestModel.EquipmentId);

            //Update new information to equipment
            equipment = UpdateEquipmentRequest.UpdateToEquipment(equipment, requestModel, room);

            try
            {
                equipment = await _repoWrapper.Equipment.UpdateAsync(equipment, equipment.EquipmentId);
            }
            catch (Exception)
            {
                throw new HttpStatusCodeException(500, "EquipmentService: Internal Server Error Occured when updating equipment");
            }

            return UpdateEquipmentResponse.CreateFromEquipment(equipment, room);
        }
    }
}