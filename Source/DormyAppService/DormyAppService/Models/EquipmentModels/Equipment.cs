using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyAppService.Models.RoomModels;

namespace DormyAppService.Models.EquipmentModels
{
    public class Equipment
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        [Required] 
        public EquipmentType EquipmentType { get; set; }

        [Column(TypeName = "Money")]
        public decimal Price { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }
    }
}