using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Utilities;
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

        //      [Required]
        public string PriorityImageUrl { get; set; }

        [Required]
        public int PriorityType { get; set; }

        public static RoomBookingRequestForm NewEntityFromRequest(SendRoomBookingRequest request, int maxDayForApproveRoomBooking)
        {
            var rejectDate = DateHelper.AddBusinessDays(DateTime.Now.AddHours(GlobalParams.TimeZone), maxDayForApproveRoomBooking);

            return new RoomBookingRequestForm()
            {
                StudentId = request.StudentId,
                CreatedDate = DateTime.Now.AddHours(GlobalParams.TimeZone),
                LastUpdated = DateTime.Now.AddHours(GlobalParams.TimeZone),
                Month = request.Month,
                Status = RequestStatus.Pending,
                TargetRoomType = request.TargetRoomType,
                IdentityCardImageUrl = request.IdentityCardImageUrl,
                PriorityImageUrl = request.PriorityImageUrl,
                StudentCardImageUrl = request.StudentCardImageUrl,
                PriorityType = request.PriorityType,
                //Set reject date to before 6pm
                RejectDate = new DateTime(rejectDate.Year, rejectDate.Month, rejectDate.Day, 17,59,0)
            };
        }
    }
}