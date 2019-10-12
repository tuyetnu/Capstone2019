using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.Entities.TicketEntities
{
    public class IssueTicket
    {
        [Key]
        public int IssueTicketId { get; set; }

        //Param
        [Required]
        public int Type { get; set; }

        //Param
        [Required]
        public int Status { get; set; }

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