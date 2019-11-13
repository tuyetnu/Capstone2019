using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.Entities.MoneyEntities
{
    public class RoomMonthlyBill
    {
        [Key]
        public int RoomMonthlyBillId { get; set; }

        [Required]
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room Room { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [Required]
        public int PreviousWaterNumber { get; set; }

        [Required]
        public int NewWaterNumber { get; set; }

        [Required]
        public int PricePerWaterId { get; set; }
        [Required]
        public Decimal WaterBill { get; set; }

        [Required]
        public int PreviousElectricityNumber { get; set; }

        [Required]
        public int NewElectricityNumber { get; set; }

        [Required]
        public int PricePerElectricityId { get; set; }
        [Required]
        public Decimal ElectricityBill { get; set; }

        [Required]
        public int PricePerRoomId { get; set; }
        [Required]
        public Decimal RoomBill { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public virtual ICollection<StudentMonthlyBill> StudentMonthlyBill { get; set; }
    }
}