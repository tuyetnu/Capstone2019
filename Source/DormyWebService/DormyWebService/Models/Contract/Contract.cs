using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Models.AccountModels;
using DormyWebService.Models.ParamModels;

namespace DormyWebService.Models.Contract
{
    public class Contract
    {
        [Key]
        public int ContractId { get; set; }

        [Required]
        public Student Student { get; set; }

        [Required]
        public Param Status { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdate { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

    }
}