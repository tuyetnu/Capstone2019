using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Utilities;

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

        public static RoomBookingRequestForm UpdateFromRequest(RoomBookingRequestForm roomBooking, EditRoomBookingRequest request)
        {
            roomBooking.LastUpdated = DateTime.Now.AddHours(7);
            roomBooking.Month = request.Month;
            roomBooking.TargetRoomType = request.TargetRoomType;
            roomBooking.IdentityCardImageUrl = request.IdentityCardImageUrl;
            roomBooking.PriorityImageUrl = request.PriorityImageUrl;
            roomBooking.StudentCardImageUrl = request.StudentCardImageUrl;
            roomBooking.PriorityType = request.PriorityType;

            return roomBooking;
        }
    }
}