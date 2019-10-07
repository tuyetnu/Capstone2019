using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using DormyAppService.Models.AccountModels;
using DormyAppService.Models.RoomModels;

namespace DormyAppService.Models.MoneyModels
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Student Student { get; set; }

        [Required]
        public RoomType RoomType { get; set; }

        [Column(TypeName = "Money")]
        public Decimal Total { get; set; }

        [Required]
        public MonthlyBill MonthlyBill { get; set; }

        [Required]
        public Decimal Percentage { get; set; }
    }
}