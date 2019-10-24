using System;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.TicketEntities;

namespace DormyWebService.ViewModels.TicketViewModels.RoomBooking.ResolveRoomBooking
{
    public class ResolveRoomBookingResponse
    {
        public int RoomBookingRequestFormId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public int Month { get; set; }
        public string Status { get; set; }
        public int StudentId { get; set; }
        public int StaffId { get; set; }
        public int TargetRoomType { get; set; }
    }
}