using System;
using System.Net;
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

        public EquipmentService(IRepositoryWrapper repoWrapper, IMapper mapper, IAdminService admin, IRoomService room,
            IParamService param)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
            _admin = admin;
            _room = room;
            _param = param;
        }

        public async Task<Equipment> FindById(int id)
        {
            var equipment = await _repoWrapper.Equipment.FindByIdAsync(id);

            //Check if there's this user in database
            if (equipment == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "EquipmentService: Equipment is not found");
            }

            return equipment;
        }

        public async Task<CreateEquipmentResponse> CreateEquipment(CreateEquipmentRequest requestModel)
        {
            //If there's a room, check if the room exists 
            if (requestModel.RoomId != null)
            {
                var room = await _room.FindById(requestModel.RoomId.Value);
            }

            var equipment = await _repoWrapper.Equipment.CreateAsync(
                CreateEquipmentRequest.NewEquipmentFromRequest(requestModel));

            return CreateEquipmentResponse.CreateFromEquipment(equipment);
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

            equipment = await _repoWrapper.Equipment.UpdateAsync(equipment, equipment.EquipmentId);

            return UpdateEquipmentResponse.CreateFromEquipment(equipment, room);
        }
    }
}