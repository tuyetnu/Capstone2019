using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.TicketEntities;
using Sieve.Attributes;

namespace DormyWebService.ViewModels.TicketViewModels.RoomBooking.SendRoomBooking
{
    public class SendRoomBookingRequest
    {
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

        public static RoomBookingRequestForm NewEntityFromReQuest(SendRoomBookingRequest request)
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