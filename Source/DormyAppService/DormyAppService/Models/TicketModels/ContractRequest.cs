using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DormyAppService.Models.AccountModels;

namespace DormyAppService.Models.TicketModels
{
    public class ContractRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Student Student { get; set; }

        public Staff Staff { get; set; }

        [Required]
        public ContractRequestStatus Status { get; set; }

        public int Month { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}