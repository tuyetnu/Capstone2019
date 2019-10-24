using System;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.TicketEntities;

namespace DormyWebService.ViewModels.TicketViewModels.RoomBooking.ResolveRoomBooking
{
    public class ResolveRoomBookingResponse
    {
        public int RoomBookingRequestFormId { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdated { get; set; }
        public int Month { get; set; }
        public string Status { get; set; }
        public int StudentId { get; set; }
        public int StaffId { get; set; }
        public int TargetRoomType { get; set; }
        public string Reason { get; set; }
    }
}