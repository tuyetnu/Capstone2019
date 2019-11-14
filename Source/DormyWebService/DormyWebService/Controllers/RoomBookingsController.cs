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
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.ImportRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.RejectRoomBooking;
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
//        [Authorize]
        [HttpGet("GetDetail/{id}")]
        public async Task<ActionResult<GetRoomBookingDetailResponse>> GetRoomBookingDetail(int id)
        {
            return await _roomBookingService.GetRoomBookingDetail(id);
        }

        /// <summary>
        /// send room booking request for student
        /// </summary>
        /// <param name="request"></param>
//        [Authorize(Roles = Role.Student)]
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
        /// Approve a room booking and arrange room, for staff
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
//        [Authorize(Roles = Role.Staff)]
        [HttpPut("ApproveRoomBooking/{id}")]
        public async Task<ActionResult<ArrangeRoomResponseStudent>> ApproveRoomBooking(int id)
        {
            return await _roomBookingService.ApproveRoomBookingRequest(id);
        }

        /// <summary>
        /// Reject a room booking, for staff
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Staff)]
        [HttpPut("RejectRoomBooking")]
        public async Task<ActionResult<bool>> RejectRoomBooking(RejectRoomBookingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _roomBookingService.RejectRoomBookingRequest(request);
        }

        /// <summary>
        /// Complete a room booking and create new contract, for staff
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Staff)]
        [HttpPut("CompleteRoomBooking/{id}")]
        public async Task<ActionResult<bool>> CompleteRoomBooking(int id)
        {
            return await _roomBookingService.CompleteRoomBookingRequest(id);
        }

        /// <summary>
        /// Import list of roomBooking and arrange room for students, for staff and admin
        /// </summary>
        /// <returns></returns>
//        [Authorize(Roles = Role.Admin + "," + Role.Staff)]
        [HttpPost("ImportRoomBooking")]
        public async Task<ActionResult<ArrangeRoomResponse>> ImportRoomBooking(List<ImportRoomBookingRequest> requests)
        {
            try
            {
                return await _roomBookingService.ImportRoomBookingRequests(requests);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}