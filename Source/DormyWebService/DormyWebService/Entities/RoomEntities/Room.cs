using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey("RoomGroup")]
        public int? RoomGroupId { get; set; }

        public RoomGroup RoomGroup {get;set; }

        [ForeignKey("Building")] 
        public int? BuildingId { get; set; }
        public Building Building { get; set; }

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
        public int CurrentNumberOfStudent { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public int RoomType { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public decimal Price { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public string RoomStatus { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public bool Gender { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Equipment> Equipments { get; set; }

        public virtual ICollection<StudentMonthlyBill> StudentMonthlyBills { get; set; }
    }
}