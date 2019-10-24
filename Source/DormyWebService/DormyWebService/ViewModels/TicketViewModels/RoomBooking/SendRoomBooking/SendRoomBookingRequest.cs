using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.TicketEntities;

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
            };
        }
    }
}