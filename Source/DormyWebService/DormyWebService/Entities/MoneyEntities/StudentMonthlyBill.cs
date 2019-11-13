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
        public DateTime PaidDate { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [Required]
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room Room { get; set; }

        [Required]
        [ForeignKey("RoomMonthlyBill")]
        public int RoomMonthlyBillId { get; set; }
        public RoomMonthlyBill RoomMonthlyBill { get; set; }

        [Required] 
        public Decimal Total { get; set; }
    }
}