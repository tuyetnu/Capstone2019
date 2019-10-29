using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Services.TicketServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.RoomViewModels.ArrangeRoom;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.EditRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.GetRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.GetRoomBookingDetail;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.ResolveRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.SendRoomBooking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
//        [Authorize(Roles = Role.Staff + "," + Role.Admin)]
        [HttpGet]
        public async Task<ActionResult<AdvancedGetRoomBookingResponse>> AdvancedGetRoomBooking(string sorts,
            string filters, int? page,
            int? pageSize)
        {
            return await _roomBookingService.AdvancedGetRoomRequest(sorts, filters, page, pageSize);
        }

        /// <summary>
        /// Get a room booking request's detail, for authorized user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetDetail/{id}")]
        public async Task<ActionResult<GetRoomBookingDetailResponse>> GetRoomBookingDetail(int id)
        {
            return await _roomBookingService.GetRoomBookingDetail(id);
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

            return await _roomBookingService.SendRequest(request);
        }

        /// <summary>
        /// Edit room booking request, for student
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Student)]
        [HttpPut]
        public async Task<ActionResult<bool>> EditRoomBooking(EditRoomBookingRequest request)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            return await _roomBookingService.EditRoomRequest(request);
        }

        /// <summary>
        /// Change status of a Room Booking Request, for staff
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Staff)]
        [HttpPut("ChangeRoomBookingStatus")]
        public async Task<ActionResult<ResolveRoomBookingResponse>> ChangeRoomBookingStatus(
            ResolveRoomBookingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (request.Status != RequestStatus.Approved && request.Status != RequestStatus.Rejected &&
                request.Status != RequestStatus.Complete)
            {
                return BadRequest("Status is Invalid, has to be Approved, Rejected or Complete");
            }

            return await _roomBookingService.ResolveRequest(request);
        }

        [HttpDelete("Debug/{RequestId}")]
        public async Task<ActionResult<bool>> DeleteRoomBooking(int RequestId)
        {
            return await _roomBookingService.DeleteRoomBooking(RequestId);
        }
    }
}