using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Repositories;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.ContractViewModels.GetContractViewModel;
using DormyWebService.ViewModels.EquipmentViewModels.GetEquipment;
using Sieve.Models;
using Sieve.Services;

namespace DormyWebService.Services.ContractServices
{
    public class ContractService : IContractService
    {
        private readonly ISieveProcessor _sieveProcessor;
        private readonly IRepositoryWrapper _repoWrapper;

        public ContractService(ISieveProcessor sieveProcessor, IRepositoryWrapper repoWrapper)
        {
            _sieveProcessor = sieveProcessor;
            _repoWrapper = repoWrapper;
        }

        public async Task<AdvancedGetContractResponse> AdvancedGetContracts(string sorts, string filters, int? page, int? pageSize)
        {
            var sieveModel = new SieveModel()
            {
                PageSize = pageSize,
                Sorts = sorts,
                Page = page,
                Filters = filters
            };

            var contracts = await _repoWrapper.Contract.FindAllAsync();

            if (contracts == null || contracts.Any() == false)
            {
                //return null if no contract is found
                return new AdvancedGetContractResponse()
                {
                    ResultList = null,
                    CurrentPage = 1,
                    TotalPage = 1
                };
            }

            var resultResponses = new List<GetContractResponse>();

            foreach (var contract in contracts)
            {
                resultResponses.Add(GetContractResponse.ResponseFromEntity(contract));
            }

            //Apply filter, sort
            var result = _sieveProcessor.Apply(sieveModel, resultResponses.AsQueryable(), applyPagination: false).ToList();

            var response = new AdvancedGetContractResponse()
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

        public async Task<List<GetContractResponse>> GetByStudentId(int id)
        {
            var contracts = await _repoWrapper.Contract.FindAllAsyncWithCondition(c => c.StudentId == id);

            if (contracts == null || contracts.Any() == false)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "ContractService: No contract is found");
            }

            return contracts.Select(GetContractResponse.ResponseFromEntity).ToList();
        }
    }
}