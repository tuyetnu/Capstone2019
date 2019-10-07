using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DormyAppService.Models.AccountModels;
using DormyAppService.Models.RoomModels;

namespace DormyAppService.Models.TicketModels
{
    public class RoomRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        //Current Status
        [Required]
        public RoomRequestStatus Status { get; set; }

        [Required]
        public Student Student { get; set; }

        public Staff Staff { get; set; }

        [Required]
        public RoomType TargetRoomType { get; set; }
    }
}