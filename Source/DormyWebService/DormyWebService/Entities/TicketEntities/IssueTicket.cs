using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.RoomEntities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sieve.Attributes;

namespace DormyWebService.Entities.TicketEntities
{
    public class IssueTicket
    {
        [Key]
        [Sieve(CanFilter = true, CanSort = true)]
        public int IssueTicketId { get; set; }

        //Param
        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public int Type { get; set; }

        //Param
        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public string Status { get; set; }

        [Required]
        [ForeignKey("Owner")]
        [Sieve(CanFilter = true, CanSort = true)]
        public int OwnerId { get; set; }
        public Student Owner { get; set; }

        [ForeignKey("Equipment")]
        [Sieve(CanFilter = true, CanSort = true)]
        public int? EquipmentId { get; set; }
        public Equipment Equipment { get; set; }

        [ForeignKey("Staff")]
        [Sieve(CanFilter = true, CanSort = true)]
        public int? StaffId { get; set; }
        public Staff Staff { get; set; }

        [Required]
        [MaxLength(100)]
        [Sieve(CanFilter = true, CanSort = true)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int? Point { get; set; }

        public string ImageUrl { get; set; }

        [ForeignKey("Room")]
        [Sieve(CanFilter = true, CanSort = true)]
        public int? RoomId { get; set; }
        public Room Room { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime LastUpdated { get; set; }
    }
}