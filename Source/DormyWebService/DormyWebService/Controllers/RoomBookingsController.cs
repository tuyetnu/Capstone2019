using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Services.TicketServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.GetRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.ResolveRoomBooking;
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
        /// Get List of Room Booking Request with condition for staff and admin
        /// </summary>
        /// <remarks>See GET /api/Rooms for examples</remarks>
        /// <param name="sorts"></param>
        /// <param name="filters"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Staff + "," + Role.Admin)]
        [HttpGet]
        public async Task<ActionResult<List<GetRoomBookingResponse>>> AdvancedGetRoomBooking(string sorts, string filters, int? page,
            int? pageSize)
        {
            try
            {
                return await _roomBookingService.AdvancedGetRoomRequest(sorts, filters, page, pageSize);
            }
            catch (HttpStatusCodeException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
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

        /// <summary>
        /// Change status of a Room Booking Request, for staff
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Staff)]
        [HttpPut]
        public async Task<ActionResult<ResolveRoomBookingResponse>> ChangeRoomBookingStatus(ResolveRoomBookingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (request.Status != RequestStatus.Approved && request.Status != RequestStatus.Rejected && request.Status != RequestStatus.Complete)
            {
                return BadRequest("Status is Invalid, has to be Approved, Rejected or Complete");
            }

            try
            {
                return await _roomBookingService.ResolveRequest(request);
            }
            catch (HttpStatusCodeException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
        }

        [HttpDelete("Debug/{RequestId}")]
        public async Task<ActionResult<bool>> DeleteRoomBooking(int RequestId)
        {
            try
            {
                return await _roomBookingService.DeleteRoomBooking(RequestId);
            }
            catch (HttpStatusCodeException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
        }
    }
}