using System;
using System.ComponentModel.DataAnnotations;
using DormyAppService.Models;
using DormyAppService.Models.TicketModels;
using DormyWebService.Models.AccountModels;
using DormyWebService.Models.EquipmentModels;
using DormyWebService.Models.ParamModels;
using DormyWebService.Models.RoomModels;

namespace DormyWebService.Entities.TicketModels
{
    public class IssueTicket
    {
        [Key]
        public int IssueTicketId { get; set; }

        [Required]
        public Param Type { get; set; }

        [Required]
        public Param Status { get; set; }

        [Required]
        public User Owner { get; set; }

        public User TargetUser { get; set; }

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