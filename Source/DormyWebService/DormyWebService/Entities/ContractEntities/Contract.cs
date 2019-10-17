using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.AccountEntities;

namespace DormyWebService.Entities.ContractEntities
{
    public class Contract
    {
        [Key]
        public int ContractId { get; set; }

        [Required]
        public Student Student { get; set; }

        //Param
        [Required]
        public string Status { get; set; }

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