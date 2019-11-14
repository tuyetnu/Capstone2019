using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Utilities;
using Microsoft.Extensions.Logging.Debug;
using Sieve.Attributes;

namespace DormyWebService.ViewModels.TicketViewModels.RoomBooking.GetRoomBooking
{
    public class GetRoomBookingResponse
    {
        //Room Booking Id
        [Sieve(CanFilter = true, CanSort = true)]
        public int RoomBookingRequestFormId { get; set; }

        //CreatedDate
        [Sieve(CanFilter = true, CanSort = true)]
        public string CreatedDate { get; set; }

        //LastUpdated
        [Sieve(CanFilter = true, CanSort = true)]
        public string LastUpdated { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime LastUpdatedDate { get; set; }

        //Month
        [Sieve(CanFilter = true, CanSort = true)]
        public int Month { get; set; }

        //Status
        [Sieve(CanFilter = true, CanSort = true)]
        public string Status { get; set; }

        //StudentId
        [Sieve(CanFilter = true, CanSort = true)]
        public int StudentId { get; set; }

        //Student Name
        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Email { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string StudentCardNumber { get; set; }
        public string StudentCardImageUrl { get; set; }
        public string IdentityCardImageUrl { get; set; }
        public string PriorityImageUrl { get; set; }
         public int PriorityType { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int TargetRoomType { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string TargetRoomTypeName { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int? RoomId { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string RoomName { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string RejectDate { get; set; }

        public static GetRoomBookingResponse ResponseFromEntity(RoomBookingRequestForm roomBooking, Student student, Entities.ParamEntities.Param roomType, Room room) 
        {
            return new GetRoomBookingResponse(){
                StudentId = roomBooking.StudentId,
                Status = roomBooking.Status,
                Name = student.Name,
                CreatedDate = roomBooking.CreatedDate.ToString(GlobalParams.DateTimeResponseFormat),
                LastUpdated = roomBooking.LastUpdated.ToString(GlobalParams.DateTimeResponseFormat),
                LastUpdatedDate = roomBooking.LastUpdated,
                Email = student.Email,
                TargetRoomType = roomBooking.TargetRoomType,
                RoomBookingRequestFormId = roomBooking.RoomBookingRequestFormId,
                Month = roomBooking.Month,
                StudentCardNumber = student.StudentCardNumber,
                StudentCardImageUrl = roomBooking.StudentCardImageUrl,
                IdentityCardImageUrl = roomBooking.IdentityCardImageUrl,
                PriorityImageUrl = roomBooking.PriorityImageUrl,
                PriorityType = roomBooking.PriorityType,
                TargetRoomTypeName = roomType.Name,
                RoomId = room?.RoomId ?? -1,
                RoomName = room?.Name ?? "null",
                RejectDate = roomBooking.RejectDate.ToString(GlobalParams.DateTimeResponseFormat)
            };
        }
    }
}