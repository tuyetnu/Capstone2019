using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.Entities.MoneyEntities
{
    public class MoneyTransaction
    {
        [Required]
        public int MoneyTransactionId { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [Required]
        public int Type { get; set; }

        [ForeignKey("Room")]
        public int? RoomId { get; set; }
        public Room Room { get; set; }

        [Required]
        public decimal OriginalBalance { get; set; }

        [Required]
        public decimal MoneyAmount { get; set; }

        [Required]
        public decimal ResultBalance { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}