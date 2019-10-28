using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Utilities;
using Sieve.Attributes;

namespace DormyWebService.ViewModels.TicketViewModels.RoomBooking.GetRoomBooking
{
    public class GetRoomBookingResponse
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int RoomBookingRequestFormId { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string CreatedDate { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string LastUpdated { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public int Month { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string Status { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public int StudentId { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string Email { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string StudentCardNumber { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public int TargetRoomType { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string TargetRoomTypeName { get; set; }

        public static GetRoomBookingResponse ResponseFromEntity(RoomBookingRequestForm roomBooking, Student student, Entities.ParamEntities.Param roomType) 
        {
            return new GetRoomBookingResponse(){
                StudentId = roomBooking.StudentId,
                Status = roomBooking.Status,
                Name = student.Name,
                CreatedDate = roomBooking.CreatedDate.ToString(GlobalParams.DateTimeResponseFormat),
                LastUpdated = roomBooking.LastUpdated.ToString(GlobalParams.DateTimeResponseFormat),
                Email = student.Email,
                TargetRoomType = roomBooking.TargetRoomType,
                RoomBookingRequestFormId = roomBooking.RoomBookingRequestFormId,
                Month = roomBooking.Month,
                StudentCardNumber = student.StudentCardNumber,
                TargetRoomTypeName = roomType.Name,
            };
        }
    }
}