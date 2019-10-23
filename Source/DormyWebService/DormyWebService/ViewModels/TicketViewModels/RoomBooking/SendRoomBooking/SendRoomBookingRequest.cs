using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.AccountEntities;

namespace DormyWebService.ViewModels.TicketViewModels.RoomBooking.SendRoomBooking
{
    public class SendRoomBookingRequest
    {
        [Required]
        private int Month { get; set; }

        [Required]
        public int TargetRoomType { get; set; }
    }
}