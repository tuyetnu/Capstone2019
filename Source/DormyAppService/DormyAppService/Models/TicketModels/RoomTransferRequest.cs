using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DormyAppService.Models.AccountModels;
using DormyAppService.Models.Enums;
using DormyAppService.Models.RoomModels;

namespace DormyAppService.Models.TicketModels
{
    public class RoomTransferRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(500)]
        public string Reason { get; set; }

        [Required]
        public Student Student { get; set; }

        public Staff Staff { get; set; }

        [Required]
        public RoomType TargetRoomType { get; set; }

        [Required]
        public RequestStatusEnum Status { get; set; }
    }
}