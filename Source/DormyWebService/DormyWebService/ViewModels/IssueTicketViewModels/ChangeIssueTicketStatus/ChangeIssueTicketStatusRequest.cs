using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.TicketEntities;
using FluentValidation;

namespace DormyWebService.ViewModels.IssueTicketViewModels.ChangeIssueTicketStatus
{
    public class ChangeIssueTicketStatusRequest
    {
        [Required]
        public int IssueTicketId { get; set; }
        [Required]
        public int StaffId { get; set; }
        [Required]
        public string Status { get; set; }

        public IssueTicket UpdateToIssueTicket(IssueTicket issueTicket)
        {
            issueTicket.StaffId = StaffId;
            issueTicket.Status = Status;
            issueTicket.LastUpdated = DateTime.Now;

            return issueTicket;
        }
    }
}