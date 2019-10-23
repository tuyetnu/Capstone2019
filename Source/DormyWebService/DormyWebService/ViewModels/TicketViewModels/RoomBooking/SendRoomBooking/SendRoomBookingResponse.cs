using System;
using DormyWebService.Entities.AccountEntities;

namespace DormyWebService.ViewModels.TicketViewModels.RoomBooking.SendRoomBooking
{
    public class SendRoomBookingResponse
    {
        public int RoomBookingRequestFormId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        private int Month { get; set; }
        public int Status { get; set; }
        public int StudentId { get; set; }
        public int TargetRoomType { get; set; }
    }
}