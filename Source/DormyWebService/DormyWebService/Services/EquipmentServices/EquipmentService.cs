using System;
using System.Collections.Generic;
using System.Linq;
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
using DormyWebService.ViewModels.EquipmentViewModels.GetEquipment;
using DormyWebService.ViewModels.EquipmentViewModels.UpdateEquipment;
using DormyWebService.ViewModels.NewsViewModels.UpdateNews;
using Sieve.Models;
using Sieve.Services;

namespace DormyWebService.Services.EquipmentServices
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IMapper _mapper;
        private readonly IAdminService _admin;
        private readonly IRoomService _room;
        private readonly IParamService _param;
        private readonly ISieveProcessor _sieveProcessor;

        public EquipmentService(IRepositoryWrapper repoWrapper, IMapper mapper, IAdminService admin, IRoomService room,
            IParamService param, ISieveProcessor sieveProcessor)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
            _admin = admin;
            _room = room;
            _param = param;
            _sieveProcessor = sieveProcessor;
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

        public async Task<List<GetEquipmentResponse>> GetEquipmentOfStudent(int studentId)
        {
            var student = await _repoWrapper.Student.FindByIdAsync(studentId);

            if (student == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "EquipmentService: Student not found");
            }

            if (student.RoomId == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "EquipmentService: Student doesn't have room'");
            }

            var equipments = (List<Equipment>) await _repoWrapper.Equipment.FindAllAsyncWithCondition(e => e.RoomId == student.RoomId);

            if (!equipments.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "EquipmentService: Equipments not found");
            }

            return equipments.Select(equipment => _mapper.Map<GetEquipmentResponse>(equipment)).ToList();
        }

        public async Task<List<GetEquipmentResponse>> AdvancedGetEquipments(string sorts, string filters, int? page, int? pageSize)
        {
            var sieveModel = new SieveModel()
            {
                PageSize = pageSize,
                Sorts = sorts,
                Page = page,
                Filters = filters
            };

            var equipment = await _repoWrapper.Equipment.FindAllAsync();

            if (equipment == null || equipment.Any() == false)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "EquipmentService: No equipment is found");
            }

            var result = _sieveProcessor.Apply(sieveModel, equipment.AsQueryable()).ToList();
            return result.Select(e=>_mapper.Map<GetEquipmentResponse>(e)).ToList();
        }
    }
}