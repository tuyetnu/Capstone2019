using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Services.TicketServices;
using DormyWebService.ViewModels.TicketViewModels.CancelContract.GetCancelContract;
using DormyWebService.ViewModels.TicketViewModels.CancelContract.SendCancelContractRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractCancelController : ControllerBase
    {
        private readonly ICancelContractService _contractService;
        public ContractCancelController(ICancelContractService contractService)
        {
            _contractService = contractService;
        }
        [Authorize(Roles = Role.Admin + "," + Role.Staff)]
        [HttpGet("AdvancedGet")]
        public async Task<ActionResult<AdvancedGetCancelContractResponse>> AdvancedGetCancelContract(string sorts, string filters, int? page, int? pageSize)
        {
            return await _contractService.AdvancedGetCancelContract(sorts, filters, page, pageSize);
        }
        /// <summary>
        /// Send renew contract for student
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Student)]
        [HttpPost]
        public async Task<ActionResult<SendCancelContractFormResponse>> SendRenewContract(SendCancelContractFormRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _contractService.SendCancelContract(request);
        }
    }
}