using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.AccountEntities;
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

        //Param
        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public int Status { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public int Month { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime StartTime { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime EndTime { get; set; }
    }
}