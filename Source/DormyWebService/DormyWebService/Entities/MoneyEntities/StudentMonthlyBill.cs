using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.Entities.MoneyEntities
{
    public class StudentMonthlyBill
    {
        [Key]
        public int StudentMonthlyBillId { get; set; }

        [Required]
        public bool IsPaid { get; set; }

        [Required]
        public Student Student { get; set; }

        [Required]
        public Room Room { get; set; }

        [ForeignKey("RoomMonthlyBill")]
        public int RoomMonthlyBillId { get; set; }

        [Required]
        public RoomMonthlyBill RoomMonthlyBill { get; set; }
        
        // WaterFee + Electricity
        [Column(TypeName = "Money")]
        public Decimal RoomUtilityFee { get; set; }

        //Phần trăm sau khi chia
        [Required]
        public Decimal Percentage { get; set; }

        //RoomUtilityFee * Percentage
        [Required]
        [Column(TypeName = "Money")]
        public Decimal UtilityFee { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public Decimal RoomFee { get; set; }

        // UtilityFee + RoomFee
        [Required] 
        [Column(TypeName = "Money")]
        public Decimal Total { get; set; }

        [Required]
        public DateTime TargetMonth { get; set; }

        [Required]
        public DateTime TargetYear { get; set; }

    }
}