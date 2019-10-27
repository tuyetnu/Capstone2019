using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Entities.TicketEntities;

namespace DormyWebService.ViewModels.IssueTicketViewModels.SendIssueTicket
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
            };
        }
    }
}