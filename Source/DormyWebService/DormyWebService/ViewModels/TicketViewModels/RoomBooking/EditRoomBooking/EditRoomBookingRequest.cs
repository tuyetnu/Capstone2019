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
            roomBooking.LastUpdated = DateTime.Now.AddHours(GlobalParams.TimeZone);
            roomBooking.Month = request.Month;
            roomBooking.TargetRoomType = request.TargetRoomType;
            //If Image is request is null don't change
            if (request.IdentityCardImageUrl != null)
            {
                roomBooking.IdentityCardImageUrl = request.IdentityCardImageUrl;
            }
            //If Image is request is null don't change
            if (request.PriorityImageUrl != null)
            {
                roomBooking.PriorityImageUrl = request.PriorityImageUrl;
            }
            //If Image is request is null don't change
            if (request.StudentCardImageUrl != null)
            {
                roomBooking.StudentCardImageUrl = request.StudentCardImageUrl;
            }
            roomBooking.PriorityType = request.PriorityType;

            return roomBooking;
        }
    }
}