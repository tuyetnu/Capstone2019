using DormyWebService.Entities.TicketEntities;
using DormyWebService.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.TicketViewModels.RoomBooking.UpdateRoomBookingImage
{
    public class UpdateRoomBookingImageRequest
    {
        public int RoomBookingFormId { get; set; }
        public string IdentityUrl { get; set; }
        public string StudentCardUrl { get; set; }
        public string PriorityUrl { get; set; }

        public static RoomBookingRequestForm UpdateFromRequest(RoomBookingRequestForm roomBooking, UpdateRoomBookingImageRequest request)
        {
            roomBooking.LastUpdated = DateTime.Now.AddHours(GlobalParams.TimeZone);
            //If Image is request is null don't change
            if (!string.IsNullOrEmpty(request.IdentityUrl))
            {
                roomBooking.IdentityCardImageUrl = request.IdentityUrl;
            }

            //If Image is request is null don't change
            if (!string.IsNullOrEmpty(request.PriorityUrl))
            {
                roomBooking.PriorityImageUrl = request.PriorityUrl;
            }

            //If Image is request is null don't change
            if (!string.IsNullOrEmpty(request.StudentCardUrl))
            {
                roomBooking.StudentCardImageUrl = request.StudentCardUrl;
            }

            return roomBooking;
        }
    }
}
