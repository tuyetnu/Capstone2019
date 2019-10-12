using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Models.RoomModels;

namespace DormyWebService.Models.MoneyModels
{
    public class RoomMonthlyBill
    {
        [Key]
        public int RoomMonthlyBillId { get; set; }

        [Required]
        public Room Room { get; set; }

        [Required]
        public bool IsPaid { get; set; }

        [Required]
        public int TargetMonth { get; set; }

        [Required]
        public int TargetYear { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [Required]
        public int PreviousWaterNumber { get; set; }

        [Required]
        public int NewWaterNumber { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal WaterBill { get; set; }

        [Required]
        public int PreviousElectricityNumber { get; set; }

        [Required]
        public int NewElectricityNumber { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal ElectricityBill { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal TotalRoomFee { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal TotalAmount { get; set; }

        public virtual ICollection<StudentMonthlyBill>  StudentMonthlyBill { get; set; }
    }
}