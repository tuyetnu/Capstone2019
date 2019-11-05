using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Repositories;
using DormyWebService.Services.ParamServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.EquipmentViewModels.GetEquipment;
using DormyWebService.ViewModels.RoomViewModels.GetAllMissingEquipmentRoom;
using Sieve.Models;
using Sieve.Services;

namespace DormyWebService.Services.RoomServices
{
    public class RoomsAndEquipmentTypesService : IRoomsAndEquipmentTypesService
    {
        private readonly IParamService _paramService;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly IRepositoryWrapper _repoWrapper;

        public RoomsAndEquipmentTypesService(IParamService paramService, ISieveProcessor sieveProcessor, IRepositoryWrapper repoWrapper)
        {
            _paramService = paramService;
            _sieveProcessor = sieveProcessor;
            _repoWrapper = repoWrapper;
        }

        public async Task<AdvancedGetAllMissingEquipmentRoomResponse> GetAllMissingEquipmentRoom(string sorts, string filters, int? page, int? pageSize)
        {
            var sieveModel = new SieveModel()
            {
                PageSize = pageSize,
                Sorts = sorts,
                Page = page,
                Filters = filters
            };

            var roomsAndEquipmentTypes = await _repoWrapper.RoomsAndEquipmentTypes.FindAllAsync();

            if (roomsAndEquipmentTypes == null || roomsAndEquipmentTypes.Any() == false)
            {
                //Return null if no record is found
                return null;
            }

            var resultResponses = new List<GetAllMissingEquipmentRoomResponse>();

            foreach (var record in roomsAndEquipmentTypes)
            {
                var equipmentType = await _repoWrapper.Param.FindByIdAsync(record.EquipmentTypeId);

                var room = await _repoWrapper.Room.FindByIdAsync(record.RoomId);

                resultResponses.Add(GetAllMissingEquipmentRoomResponse.ResponseFromEntity(record, room, equipmentType));
            }

            //Apply filter, sort
            var result = _sieveProcessor.Apply(sieveModel, resultResponses.AsQueryable(), applyPagination: false).ToList();

            var response = new AdvancedGetAllMissingEquipmentRoomResponse()
            {
                CurrentPage = page ?? 1,
                TotalPage = (int)Math.Ceiling((double)result.Count / pageSize ?? 1),
                //Apply pagination
                ResultList = _sieveProcessor
                    .Apply(sieveModel, result.AsQueryable(), applyFiltering: false, applySorting: false).ToList()
            };

            //Return List of result
            return response;
        }
    }
}