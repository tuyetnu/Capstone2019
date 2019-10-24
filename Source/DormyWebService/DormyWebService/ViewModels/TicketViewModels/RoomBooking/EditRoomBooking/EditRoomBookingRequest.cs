using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.TicketEntities;

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

        public static RoomBookingRequestForm NewEntityFromRequest(EditRoomBookingRequest request)
        {
            return new RoomBookingRequestForm()
            {
                StudentId = request.StudentId,
                CreatedDate = DateTime.Now,
                LastUpdated = DateTime.Now,
                Month = request.Month,
                Status = RequestStatus.Pending,
                TargetRoomType = request.TargetRoomType,
                IdentityCardImageUrl = request.IdentityCardImageUrl,
                PriorityImageUrl = request.PriorityImageUrl,
                StudentCardImageUrl = request.StudentCardImageUrl,
                PriorityType = request.PriorityType,
            };
        }
    }
}