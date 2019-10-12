using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.MoneyEntities;

namespace DormyWebService.Entities.RoomEntities
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

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

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Equipment> Equipments { get; set; }

        public virtual ICollection<StudentMonthlyBill> StudentMonthlyBills { get; set; }
    }
}