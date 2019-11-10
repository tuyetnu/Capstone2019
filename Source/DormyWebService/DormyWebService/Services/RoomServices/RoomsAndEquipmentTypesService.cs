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

        public AdvancedGetAllMissingEquipmentRoomResponse GetAllMissingEquipmentRoomByBuildingId(SieveModel sieveModel, int buildingId)
        {
            var resultResponses = _repoWrapper.RoomsAndEquipmentTypes.FindAllAndIncludeByBuildingId(buildingId);

            if (resultResponses == null)
            {
                return null;
            }

            //Apply filter, sort
            var result = _sieveProcessor.Apply(sieveModel, resultResponses.AsQueryable(), applyPagination: false).ToList();

            var response = new AdvancedGetAllMissingEquipmentRoomResponse()
            {
                CurrentPage = sieveModel.Page ?? 1,
                TotalPage = (int)Math.Ceiling((double)result.Count / sieveModel.PageSize ?? 1),
                //Apply pagination
                ResultList = _sieveProcessor
                    .Apply(sieveModel, result.AsQueryable(), applyFiltering: false, applySorting: false).ToList()
            };
            return response;
        }
    }
}