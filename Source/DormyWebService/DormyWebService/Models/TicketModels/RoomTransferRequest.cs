using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Models.AccountModels;
using DormyWebService.Models.ParamModels;
using DormyWebService.Models.RoomModels;

namespace DormyWebService.Models.TicketModels
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
        public Param Status { get; set; }
    }
}