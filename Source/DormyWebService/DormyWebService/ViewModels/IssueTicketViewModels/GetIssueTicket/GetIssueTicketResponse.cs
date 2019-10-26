using System;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Utilities;

namespace DormyWebService.ViewModels.IssueTicketViewModels.GetIssueTicket
{
    public class GetIssueTicketResponse
    {
        public int IssueTicketId { get; set; }
        public int Type { get; set; }
        public string Status { get; set; }
        public int OwnerId { get; set; }
        public int? EquipmentId { get; set; }
        public int? StaffId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Point { get; set; }
        public string ImageUrl { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdated { get; set; }

        public static GetIssueTicketResponse ResponseFromEntity(IssueTicket issueTicket)
        {
            return new GetIssueTicketResponse()
            {
                Status = issueTicket.Status,
                CreatedDate = issueTicket.CreatedDate.ToString(GlobalParams.DateTimeResponseFormat),
                LastUpdated = issueTicket.LastUpdated.ToString(GlobalParams.DateTimeResponseFormat),
                EquipmentId = issueTicket.EquipmentId,
                Description = issueTicket.Description,
                OwnerId = issueTicket.OwnerId,
                ImageUrl = issueTicket.ImageUrl,
                StaffId = issueTicket.StaffId,
                Title = issueTicket.Title,
                Type = issueTicket.Type,
                IssueTicketId = issueTicket.IssueTicketId,
                Point = issueTicket.Point
            };
        }
    }
}