using System.Collections.Generic;

namespace DormyWebService.ViewModels.TicketViewModels.RoomBooking.GetRoomBooking
{
    public class AdvancedGetRoomBookingResponse
    {
        public List<GetRoomBookingResponse> ResultList { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
    }
}