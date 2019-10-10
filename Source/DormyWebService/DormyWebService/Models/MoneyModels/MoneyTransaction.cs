using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Models.AccountModels;
using DormyWebService.Models.RoomModels;

namespace DormyWebService.Models.MoneyModels
{
    public class MoneyTransaction
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public Student Student { get; set; }

        [Required]
        public Room Room { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal OriginalBalance { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal MoneyAmount { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal ResultBalance { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}