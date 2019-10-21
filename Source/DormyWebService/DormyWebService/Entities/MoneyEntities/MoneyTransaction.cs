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
        public Student Student { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
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