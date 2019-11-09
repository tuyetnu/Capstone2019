using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Services.TicketServices;
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

        [Authorize(Roles = Role.Student)]
        [HttpPost]
        public async Task<ActionResult<SendCancelContractFormResponse>> SendRenewContract(SendCancelContractFormRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _contractService.SendRenewContract(request);
        }
    }
}