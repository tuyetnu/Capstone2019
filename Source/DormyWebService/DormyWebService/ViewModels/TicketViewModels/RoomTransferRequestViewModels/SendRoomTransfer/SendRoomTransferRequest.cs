using System.ComponentModel.DataAnnotations;

namespace DormyWebService.ViewModels.TicketViewModels.RoomTransferRequestViewModels.SendRoomTransfer
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