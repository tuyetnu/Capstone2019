using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormyWebService.Entities.MoneyEntities
{
    public class PricePerUnit
    {
        [Key]
        public int PricePerUnitId { get; set; }

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