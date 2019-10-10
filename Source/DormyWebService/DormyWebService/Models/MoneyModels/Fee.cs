using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormyWebService.Models.MoneyModels
{
    public class Fee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [Required]
        public int TargetMonth { get; set; }

        [Required]
        public int TargetYear { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public Decimal WaterPricePerUnit { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public Decimal ElectricityPricePerUnit { get; set; }
    }
}