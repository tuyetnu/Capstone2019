using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.TicketEntities;

namespace DormyWebService.ViewModels.TicketViewModels.IssueTicketViewModels.EditIssueTicket
{
    public class EditIssueTicketRequest
    {
        [Required]
        public int IssueTicketId { get; set; }

        [Required]
        public int Type { get; set; }

        public int EquipmentId { get; set; }

        public int TargetStudentId { get; set; }

        [Required]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

//        public static IssueTicket EntityFromRequest(IssueTicket issueTicket, EditIssueTicketRequest request)
//        {
//            issueTicket.Type = request.Type;
//            issueTicket.EquipmentId = request.EquipmentId < 0 ? null : request.EquipmentId;
//        }
    }
}