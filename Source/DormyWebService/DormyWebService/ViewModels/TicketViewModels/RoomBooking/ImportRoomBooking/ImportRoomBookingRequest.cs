using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Utilities;

namespace DormyWebService.ViewModels.TicketViewModels.RoomBooking.ImportRoomBooking
{
    public class ImportRoomBookingRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        public int TargetRoomType { get; set; }

        [Required]
        public int PriorityType { get; set; }

        public static RoomBookingRequestForm EntityFromRequest(ImportRoomBookingRequest request, int studentId, int maxDayForApproveRoomBooking)
        {
            var rejectDate = DateHelper.AddBusinessDays(DateTime.Now.AddHours(7), maxDayForApproveRoomBooking - 1);

            return new RoomBookingRequestForm()
            {
                StudentId = studentId,
                CreatedDate = DateTime.Now.AddHours(GlobalParams.TimeZone),
                LastUpdated = DateTime.Now.AddHours(GlobalParams.TimeZone),
                Month = request.Month,
                Status = RequestStatus.Approved,
                TargetRoomType = request.TargetRoomType,
                PriorityType = request.PriorityType,
                //Set reject date to before 6pm
                RejectDate = new DateTime(rejectDate.Year, rejectDate.Month, rejectDate.Day, 17, 59, 0)
            };
        }
    }
}