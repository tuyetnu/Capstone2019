using System;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.TicketEntities;

namespace DormyWebService.ViewModels.TicketViewModels.RoomBooking.ResolveRoomBooking
{
    public class ResolveRoomBookingResponse
    {
        public int RoomBookingRequestFormId { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }
}