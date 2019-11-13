using DormyWebService.Entities.ParamEntities;
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
        [ForeignKey("Param")]
        public int TypeId { get; set; }
        public Param Param { get; set; }

        [Required]
        public Decimal Price { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}