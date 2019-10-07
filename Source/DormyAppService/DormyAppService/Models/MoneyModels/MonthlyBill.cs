using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using DormyAppService.Models.RoomModels;

namespace DormyAppService.Models.MoneyModels
{
    public class MonthlyBill
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Room Room { get; set; }

        [Required]
        public bool IsPaid { get; set; }

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
        public decimal WaterPrice { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal WaterBill { get; set; }

        [Required]
        public int PreviousElectricityNumber { get; set; }

        [Required]
        public int NewElectricityNumber { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal ElectricityPrice { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal ElectricityBill { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal RoomFee { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal TotalAmount { get; set; }
    }
}