using System.ComponentModel.DataAnnotations;

namespace DormyWebService.ViewModels.TicketViewModels.RoomBooking.RejectRoomBooking
{
    public class RejectRoomBookingRequest
    {
        [Required]
        public int RoomBookingId { get; set; }

        [Required]
        public string Reason { get; set; }
    }
}