namespace DormyWebService.ViewModels.TicketViewModels.RoomBooking.ResolveRoomBooking
{
    public class ResolveRoomBookingRequest
    {
        public int RoomBookingRequestFormId { get; set; }
        public string Status { get; set; }
        public int StaffId { get; set; }
        public string Reason { get; set; }
    }
}