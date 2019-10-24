using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.AccountEntities;

namespace DormyWebService.ViewModels.TicketViewModels.RoomBooking.SendRoomBooking
{
    public class SendRoomBookingResponse
    {
        public int RoomBookingRequestFormId { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdated { get; set; }
        private int Month { get; set; }
        public string Status { get; set; }
        public int StudentId { get; set; }
        public int TargetRoomType { get; set; }
        public string IdentityCardImageUrl { get; set; }
        public string StudentCardImageUrl { get; set; }
        public int PriorityType { get; set; }
        public string PriorityImageUrl { get; set; }
    }
}