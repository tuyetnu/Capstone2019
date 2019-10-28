using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

        public async Task<List<GetContractResponse>> AdvancedGetContracts(string sorts, string filters, int? page, int? pageSize)
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
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "ContractService: No contract is found");
            }

            var result = _sieveProcessor.Apply(sieveModel, contracts.AsQueryable()).ToList();
            return result.Select(GetContractResponse.ResponseFromEntity).ToList();
        }
    }
}