using System;
using System.ComponentModel.DataAnnotations;
using DormyAppService.Models.AccountModels;
using DormyAppService.Models.EquipmentModels;
using DormyAppService.Models.RoomModels;

namespace DormyAppService.Models.TicketModels
{
    public class IssueTicket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public IssueType Type { get; set; }

        [Required]
        public IssueStatus Status { get; set; }

        [Required]
        public ApplicationUser Owner { get; set; }

        public ApplicationUser TargetUser { get; set; }

        public Equipment Equipment { get; set; }

        public Staff Staff { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int Point { get; set; }

        public string ImageUrl { get; set; }

        public Room Room { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }
    }
}