using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Services.TicketServices;
using DormyWebService.ViewModels.TicketViewModels.RoomTransfer.ApproveRoomTransfer;
using DormyWebService.ViewModels.TicketViewModels.RoomTransfer.GetRoomTransfer;
using DormyWebService.ViewModels.TicketViewModels.RoomTransfer.SendRoomTransfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTransferController : ControllerBase
    {
        private readonly IRoomTransferService _roomTransferService;
        public RoomTransferController(IRoomTransferService roomTransferService)
        {
            _roomTransferService = roomTransferService;
        }

        /// <summary>
        /// Get Room Transfer Requests with conditions, for staff and admin
        /// </summary>
        /// <param name="sorts"></param>
        /// <param name="filters"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Admin + "," + Role.Staff)]
        [HttpGet("AdvancedGetRoomTransfer")]
        public async Task<ActionResult<AdvancedGetRoomTransferResponse>> AdvancedGetRoomTransfer(string sorts, string filters, int? page, int? pageSize)
        {
            return await _roomTransferService.AdvancedGetRoomTransfer(sorts, filters, page, pageSize);
        }

        /// <summary>
        /// send room transfer request for student
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Student)]
        [HttpPost]
        public async Task<ActionResult<SendRoomTransferRespone>> SendRoomTransfer(SendRoomTransferRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _roomTransferService.SendRequest(request);
        }

        /// <summary>
        /// Approve room transfer request and find room for student, for staff
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Staff)]
        [HttpPut("ApproveRoomTransfer/{id}")]
        public async Task<ActionResult<ApproveRoomTransferResponse>> ApproveRoomTransfer(int id)
        {
            return await _roomTransferService.ApproveRoomTransfer(id);
        }

    }
}