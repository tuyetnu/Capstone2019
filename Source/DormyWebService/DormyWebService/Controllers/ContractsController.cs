using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.ContractEntities;
using DormyWebService.Services.ContractServices;
using DormyWebService.ViewModels.ContractViewModels.GetContractViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractsController(IContractService contractService)
        {
            _contractService = contractService;
        }

        /// <summary>
        /// Get contracts with condition, for admin and staff
        /// </summary>
        /// <param name="sorts"></param>
        /// <param name="filters"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Admin + "," + Role.Staff)]
        [HttpGet("AdvancedGet")]
        public async Task<ActionResult<List<GetContractResponse>>> AdvancedGetContracts(string sorts, string filters, int? page,
            int? pageSize)
        {
            return await _contractService.AdvancedGetContracts(sorts, filters, page, pageSize);
        }
    }
}