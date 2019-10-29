using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.TicketEntities;

namespace DormyWebService.ViewModels.TicketViewModels.IssueTicketViewModels.SendIssueTicket
{
    public class SendIssueTicketRequest
    {
        //Param
        [Required]
        public int Type { get; set; }

        [Required]
        public int OwnerId { get; set; }

        public int EquipmentId { get; set; }

        public int TargetStudentId { get; set; }

        [Required]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public static IssueTicket EntityFromRequest(SendIssueTicketRequest request)
        {
            int? equipmentId = null;
            int? targetStudentId = null;

            if (request.EquipmentId >= 0)
            {
                equipmentId = request.EquipmentId;
            }

            if (request.TargetStudentId >= 0)
            {
                targetStudentId = request.TargetStudentId;
            }

            return new IssueTicket()
            {
                CreatedDate = DateTime.Now,
                LastUpdated = DateTime.Now,
                Description = request.Description,
                EquipmentId = equipmentId,
                ImageUrl = request.ImageUrl,
                OwnerId = request.OwnerId,
                //Set Status to pending
                Status = IssueStatus.Pending,
                Type = request.Type,
                TargetStudentId = targetStudentId
            };
        }
    }
}