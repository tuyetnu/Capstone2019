using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.ContractEntities;
using Sieve.Attributes;

namespace DormyWebService.Entities.TicketEntities
{
    public class ContractRenewalForm
    {
        [Key]
        [Sieve(CanFilter = true, CanSort = true)]
        public int ContractRenewalFormId { get; set; }

        [Required]
        [ForeignKey("Student")]
        [Sieve(CanFilter = true, CanSort = true)]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [ForeignKey("Staff")]
        [Sieve(CanFilter = true, CanSort = true)]
        public int? StaffId { get; set; }
        public Staff Staff { get; set; }

        [Required]
        [ForeignKey("Contract")]
        [Sieve(CanFilter = true, CanSort = true)]
        public int ContractId { get; set; }
        public Contract Contract { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public string Status { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public int Month { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime LastUpdated { get; set; }

    }
}