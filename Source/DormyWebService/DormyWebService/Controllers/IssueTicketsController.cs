using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Services.TicketServices;
using DormyWebService.ViewModels.IssueTicketViewModels.GetIssueTicket;
using DormyWebService.ViewModels.IssueTicketViewModels.SendIssueTicket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueTicketsController : ControllerBase
    {
        private IIssueTicketService _issueTicketService;

        public IssueTicketsController(IIssueTicketService issueTicketService)
        {
            _issueTicketService = issueTicketService;
        }

        /// <summary>
        /// Find IssueTickets with condition for staff and admin
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Role.Staff + "," + Role.Admin)]
        [HttpGet("AdvancedGet")]
        public async Task<ActionResult<List<GetIssueTicketResponse>>> AdvancedGetRoomBooking(string sorts, string filters, int? page, int? pageSize)
        {
            return await _issueTicketService.AdvancedGetIssueTicket(sorts, filters, page, pageSize);
        }

        /// <summary>
        /// Send Issue Ticket for student
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Student)]
        [HttpPost]
        public async Task<ActionResult<SendIssueTicketReponse>> SendIssueTicket(SendIssueTicketRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _issueTicketService.SendTicket(request);
        }
    }
}