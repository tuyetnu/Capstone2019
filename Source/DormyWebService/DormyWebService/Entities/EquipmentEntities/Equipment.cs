using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.Entities.EquipmentEntities
{
    public class Equipment
    {
        [Key]
        public int EquipmentId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [ForeignKey("Room")]
        public int? RoomId { get; set; }

        public virtual Room Room { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }
    }
}