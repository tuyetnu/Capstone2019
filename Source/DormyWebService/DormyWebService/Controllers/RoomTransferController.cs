using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Services.TicketServices;
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
    }
}