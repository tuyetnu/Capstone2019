using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.AccountEntities;

namespace DormyWebService.ViewModels.RoomTransferRequestViewModels.SendRoomTransfer
{
    public class SendRoomTransferRequest
    {
        [Required]
        [MinLength(3)]
        [MaxLength(500)]
        public string Reason { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int TargetRoomId { get; set; }
    }
}