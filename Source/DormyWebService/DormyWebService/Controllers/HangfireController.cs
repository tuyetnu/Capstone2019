using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Services.TicketServices;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangFireController : ControllerBase
    {
        private IRoomBookingService _roomBookingService;
        private IIssueTicketService _issueTicketService;
        public HangFireController(IRoomBookingService roomBookingService, IIssueTicketService issueTicketService)
        {
            _roomBookingService = roomBookingService;
            _issueTicketService = issueTicketService;
        }

        /// <summary>
        /// Start or update auto reject room bookings, for admin, go to /hangfire for HangFire's dashboard
        /// </summary>
        /// <param name="time">
        /// "* * * * *" Every Minute
        /// "0 18 * * *" Everyday at 6pm
        /// </param>
        /// <remarks>Authentication disabled for debug purposes</remarks>
        /// <returns></returns>
//        [Authorize(Roles = Role.Admin)]
        [HttpGet("AutoRejectRoomBooking/{time}")]
        public IActionResult StartAutoRejectJob(string time)
        {
            RecurringJob.AddOrUpdate("AutoRejectRoomBooking", () => _roomBookingService.AutoRejectRoomBooking(), time);
            return Ok();
        }

        /// <summary>
        /// To cancel a job by it's name, for admin, go to /hangfire for HangFire's dashboard
        /// </summary>
        /// <param name="recurringJobName"></param>
        /// <remarks>Authentication disabled for debug purposes</remarks>
        /// <returns></returns>
//        [Authorize(Roles = Role.Admin)]
        [HttpDelete]
        public IActionResult CancelRecurringJob(string recurringJobName)
        {
            RecurringJob.RemoveIfExists(recurringJobName);
            return Ok();
        }
    }
}