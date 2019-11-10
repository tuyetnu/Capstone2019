using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Services.TicketServices;
using DormyWebService.ViewModels.EquipmentViewModels.GetEquipment;
using DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.ApproveRenewContract;
using DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.GetRenewContract;
using DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.RejectRenewContract;
using DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.SendRenewContractRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractRenewalController : ControllerBase
    {
        private readonly IRenewContractService _contractService;

        public ContractRenewalController(IRenewContractService contractService)
        {
            _contractService = contractService;
        }

        /// <summary>
        /// Get ContractRenewalForms with conditions, for staff and admin
        /// </summary>
        /// <param name="sorts"></param>
        /// <param name="filters"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        //[Authorize(Roles = Role.Admin + "," + Role.Staff)]
        [HttpGet("AdvancedGet")]
        public async Task<ActionResult<AdvancedGetRenewContractResponse>> AdvancedGetRenewContract(string sorts, string filters, int? page, int? pageSize)
        {
            return await _contractService.AdvancedGetRenewContract(sorts, filters, page, pageSize);
        }

        /// <summary>
        /// Send contract renewal request, for student
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Student)]
        [HttpPost]
        public async Task<ActionResult<SendRenewContractRequestResponse>> SendRenewContract(SendRenewContractRequestRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _contractService.SendRenewContract(request);
        }
        /// <summary>
        /// Approve renew contract for Staff
        /// </summary>
        /// <param name="approveRenew"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Staff)]
        [HttpPut("ApproveContractRenewal")]
        public async Task<ActionResult<ApproveRenewContractResponse>> ApproveRenewContract(ApproveRenewContractRequest approveRenew)
        {
            return await _contractService.ApproveContractRenewal(approveRenew);
        }
        /// <summary>
        /// Reject renew contract request for Staff
        /// </summary>
        /// <param name="rejectRenew"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Staff)]
        [HttpPut("RejectContractRenewal")]
        public async Task<ActionResult<RejectRenewContractResponse>> RejectRenewContract(RejectRenewContractRequest rejectRenew)
        {
            return await _contractService.RejectContractRenewal(rejectRenew);
        }

    }
}