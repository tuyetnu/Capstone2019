using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Models.AccountModels;
using DormyWebService.Models.Enums;

namespace DormyWebService.Models.TicketModels
{
    public class CancelContractRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [Required]
        public DateTime CancelationDate { get; set; }

        [Required]
        public Student Student { get; set; }

        public Staff Staff { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(500)]
        public string Reason { get; set; }

        [Required]
        public RequestStatusEnum Status { get; set; }
    }
}