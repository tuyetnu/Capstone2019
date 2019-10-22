using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.MoneyEntities;
using Sieve.Attributes;

namespace DormyWebService.Entities.RoomEntities
{
    public class Room
    {
        [Key]
        [Sieve(CanFilter = true, CanSort = true)]
        public int RoomId { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }

        [MaxLength(100)]
        [Sieve(CanFilter = true, CanSort = true)]
        public string Description { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime LastUpdated { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public int Capacity { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public int RoomType { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public decimal Price { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public string RoomStatus { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Equipment> Equipments { get; set; }

        public virtual ICollection<StudentMonthlyBill> StudentMonthlyBills { get; set; }
    }
}