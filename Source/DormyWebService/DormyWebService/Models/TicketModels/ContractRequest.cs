using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Models.AccountModels;
using DormyWebService.Models.Enums;

namespace DormyWebService.Models.TicketModels
{
    public class ContractRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Student Student { get; set; }

        public Staff Staff { get; set; }

        [Required]
        public RequestStatusEnum Status { get; set; }

        public int Month { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}