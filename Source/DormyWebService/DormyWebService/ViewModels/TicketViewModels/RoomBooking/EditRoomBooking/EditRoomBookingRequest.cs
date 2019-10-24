using System.ComponentModel.DataAnnotations;

namespace DormyWebService.ViewModels.TicketViewModels.RoomBooking.EditRoomBooking
{
    public class EditRoomBookingRequest
    {
        [Required]
        public int RoomBookingRequestFormId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        public int TargetRoomType { get; set; }

        [Required]
        public string IdentityCardImageUrl { get; set; }

        [Required]
        public string StudentCardImageUrl { get; set; }

        [Required]
        public int PriorityType { get; set; }

        [Required]
        public string PriorityImageUrl { get; set; }
    }
}