using System.ComponentModel.DataAnnotations;

namespace DormyWebService.ViewModels.TicketViewModels.RoomBooking.BulkRoomBooking
{
    public class BulkRoomBookingRequest
    {
        [Required]
        public int Email { get; set; }

        [Required]
        public int RoomTypeId { get; set; }

        [Required]
        public int PriorityType { get; set; }

        [Required]
        public int Month { get; set; }
    }
}