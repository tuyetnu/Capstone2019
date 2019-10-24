using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Services.TicketServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.SendRoomBooking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomBookingsController : ControllerBase
    {
        private readonly IRoomBookingService _roomBookingService;

        public RoomBookingsController(IRoomBookingService roomBookingService)
        {
            _roomBookingService = roomBookingService;
        }

        /// <summary>
        /// send room booking request for student
        /// </summary>
        /// <param name="request"></param>
        [Authorize(Roles = Role.Student)]
        [HttpPost]
        public async Task<ActionResult<SendRoomBookingResponse>> SendRoomBooking(SendRoomBookingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return await _roomBookingService.SendRequest(request);
            }
            catch (HttpStatusCodeException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
        }
    }
}