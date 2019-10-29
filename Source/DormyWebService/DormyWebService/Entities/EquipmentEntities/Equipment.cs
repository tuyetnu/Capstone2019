using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.RoomEntities;
using Sieve.Attributes;

namespace DormyWebService.Entities.EquipmentEntities
{
    public class Equipment
    {
        [Key]
        [Sieve(CanFilter = true, CanSort = true)]
        public int EquipmentId { get; set; }

        [Required]
        [MaxLength(50)]
        [Sieve(CanFilter = true, CanSort = true)]
        public string Code { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public int EquipmentTypeId { get; set; }

        [ForeignKey("Room")]
        [Sieve(CanFilter = true, CanSort = true)]
        public int? RoomId { get; set; }

        public virtual Room Room { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public string Status { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public decimal Price { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime LastUpdated { get; set; }
    }
}