using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.ContractEntities;
using Sieve.Attributes;

namespace DormyWebService.Entities.TicketEntities
{
    public class CancelContractForm
    {
        [Key]
        public int CancelContractFormId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [Required]
        public DateTime CancelationDate { get; set; }

        [Required]
        [ForeignKey("Student")]
        [Sieve(CanFilter = true, CanSort = true)]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [ForeignKey("Staff")]
        [Sieve(CanFilter = true, CanSort = true)]
        public int? StaffId { get; set; }
        public Staff Staff { get; set; }

        [ForeignKey("Contract")]
        [Sieve(CanFilter = true, CanSort = true)]
        public int ContractId { get; set; }
        public Contract Contract { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(500)]
        public string Reason { get; set; }

        //Param
        [Required]
        public string Status { get; set; }
    }
}