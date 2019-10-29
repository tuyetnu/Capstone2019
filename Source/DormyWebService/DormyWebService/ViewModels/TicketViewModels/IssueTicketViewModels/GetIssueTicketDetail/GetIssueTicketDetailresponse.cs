using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Utilities;
using Sieve.Attributes;

namespace DormyWebService.ViewModels.TicketViewModels.IssueTicketViewModels.GetIssueTicketDetail
{
    public class GetIssueTicketDetailResponse
    {
        public int IssueTicketId { get; set; }
        public int Type { get; set; }
        public string TypeName { get; set; }
        public string Status { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerEmail { get; set; }
        public int? EquipmentId { get; set; }
        public int? TargetStudentId { get; set; }
        public string TargetStudentName { get; set; }
        public string TargetStudentEmail { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdated { get; set; }

        public static GetIssueTicketDetailResponse ResponseFromEntity(IssueTicket issueTicket, Student owner,
            Student targetStudent, Entities.ParamEntities.Param type)
        {
            return new GetIssueTicketDetailResponse()
            {
                IssueTicketId = issueTicket.IssueTicketId,
                Status = issueTicket.Status,
                CreatedDate = issueTicket.CreatedDate.ToString(GlobalParams.DateTimeResponseFormat),
                LastUpdated = issueTicket.LastUpdated.ToString(GlobalParams.DateTimeResponseFormat),
                EquipmentId = issueTicket.EquipmentId,
                Description = issueTicket.Description,
                ImageUrl = issueTicket.ImageUrl,

                Type = type.ParamId,
                TypeName = type.Name,

                OwnerId = owner.StudentId,
                OwnerEmail = owner.Email,
                OwnerName = owner.Name,

                TargetStudentId = targetStudent?.StudentId,
                TargetStudentEmail = targetStudent?.Email,
                TargetStudentName = targetStudent?.Name
            };
        }
    }
}