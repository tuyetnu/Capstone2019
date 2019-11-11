using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Services.TicketServices;
using DormyWebService.ViewModels.TicketViewModels.CancelContract;
using DormyWebService.ViewModels.TicketViewModels.CancelContract.ApproveCancelContract;
using DormyWebService.ViewModels.TicketViewModels.CancelContract.GetCancelContract;
using DormyWebService.ViewModels.TicketViewModels.CancelContract.RejectCancelContract;
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
        /// <summary>
        /// Approve cancel contract for Staff
        /// </summary>
        /// <param name="approveCancel"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Staff)]
        [HttpPut("ApproveCancelContract")]
        public async Task<ActionResult<ApproveCancelContractResponse>> ApproveCancleContract(ApproveCancelContractRequest approveCancel)
        {
            return await _contractService.ApproveContractCancel(approveCancel);
        }
        /// <summary>
        /// Reject cancel contract for Staff
        /// </summary>
        /// <param name="rejectCancel"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Staff)]
        [HttpPut("RejectContractCancel")]
        public async Task<ActionResult<RejectCancelContractRespone>> RejectCancelContract(RejectCancelContractRequest rejectCancel)
        {
            return await _contractService.RejectCancelContract(rejectCancel);
        }
        /// <summary>
        /// get cancel contract detail for Authorized user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetDetail/{id}")]
        public async Task<ActionResult<GetCancelContractDetail>> GetRoomBookingDetail(int id)
        {
            return await _contractService.GetCancelContractDetail(id);
        }
    }
}