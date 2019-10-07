using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DormyAppService.Models.AccountModels;
using DormyAppService.Models.EquipmentModels;

namespace DormyAppService.Models.RoomModels
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public RoomType RoomType { get; set; }

        public ICollection<Student> Students { get; set; }

        public ICollection<Equipment> Equipments { get; set; }

    }
}