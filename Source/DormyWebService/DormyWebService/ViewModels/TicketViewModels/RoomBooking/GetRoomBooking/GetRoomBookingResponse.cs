using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.AccountEntities;

namespace DormyWebService.ViewModels.TicketViewModels.RoomBooking.GetRoomBooking
{
    public class GetRoomBookingResponse
    {
        public int RoomBookingRequestFormId { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdated { get; set; }
        public int Month { get; set; }
        public string Status { get; set; }
        public int StudentId { get; set; }
        public int? StaffId { get; set; }
        public int TargetRoomType { get; set; }
        public string Reason { get; set; }
    }
}