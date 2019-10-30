using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Services.TicketServices;
using DormyWebService.ViewModels.TicketViewModels.IssueTicketViewModels.ChangeIssueTicketStatus;
using DormyWebService.ViewModels.TicketViewModels.IssueTicketViewModels.EditIssueTicket;
using DormyWebService.ViewModels.TicketViewModels.IssueTicketViewModels.GetIssueTicket;
using DormyWebService.ViewModels.TicketViewModels.IssueTicketViewModels.GetIssueTicketDetail;
using DormyWebService.ViewModels.TicketViewModels.IssueTicketViewModels.SendIssueTicket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueTicketsController : ControllerBase
    {
        private readonly IIssueTicketService _issueTicketService;

        public IssueTicketsController(IIssueTicketService issueTicketService)
        {
            _issueTicketService = issueTicketService;
        }

        /// <summary>
        /// Get Detail of an IssueTicket, for Authorized Users
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetIssueTicketDetailResponse>> GetIssueTicketDetail(int id)
        {
            return await _issueTicketService.GetIssueTicketDetail(id);
        }

        [Authorize]
        [HttpGet("GetByStudent/{id}")]
        public async Task<ActionResult<List<GetIssueTicketResponse>>> GetByStudent(int id)
        {
            return await _issueTicketService.GetByStudent(id);
        }

        /// <summary>
        /// Find IssueTickets with condition for staff and admin
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Role.Staff + "," + Role.Admin)]
        [HttpGet("AdvancedGet")]
        public async Task<ActionResult<AdvancedGetIssueTicketResponse>> AdvancedGetIssueTicket(string sorts, string filters, int? page, int? pageSize)
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
        public async Task<ActionResult<SendIssueTicketResponse>> SendIssueTicket(SendIssueTicketRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _issueTicketService.SendTicket(request);
        }

        /// <summary>
        /// Edit Issue Ticket, for Student
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Student)]
        [HttpPut]
        public async Task<ActionResult<bool>> EditIssueTicket(EditIssueTicketRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _issueTicketService.EditIssueTicket(request);
        }

        /// <summary>
        /// Change a issue ticket, for staff
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Staff)]
        [HttpPut("ChangeIssueTicketStatus")]
        public async Task<ActionResult<ChangeIssueTicketStatusResponse>> ChangeIssueTicketStatus(ChangeIssueTicketStatusRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!IssueStatus.IsIssueStatus(request.Status))
            {
                return BadRequest("Status is invalid. Must be" + IssueStatus.ListAllStatuses());
            }

            return await _issueTicketService.ChangeIssueTicketStatus(request);
        }
    }
}