using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.TicketEntities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DormyWebService.Entities.ContractEntities
{
    public class Contract
    {
        [Key]
        public int ContractId { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

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


        public virtual ICollection<ContractRenewalForm> ContractRenewalForms { get; set; }
    }
}