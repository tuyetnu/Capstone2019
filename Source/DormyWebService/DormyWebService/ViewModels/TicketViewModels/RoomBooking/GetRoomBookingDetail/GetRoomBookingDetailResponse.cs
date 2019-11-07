using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Utilities;

namespace DormyWebService.ViewModels.TicketViewModels.RoomBooking.GetRoomBookingDetail
{
    public class GetRoomBookingDetailResponse
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string IdentityCardNumber { get; set; }
        public string StudentCardNumber { get; set; }
        public string BirthDay { get; set; }
        public bool Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int Term { get; set; }
        public int PriorityTypeId { get; set; }
        public string PriorityType { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomType { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdated { get; set; }
        public string RestrictDate { get; set; }

        public int RoomBookingId { get; set; }
        public int Month { get; set; }
        public string PriorityImageUrl { get; set; }
        public string IdentityCardImageUrl { get; set; }
        public string StudentCardImageUrl { get; set; }
        public int? RoomId { get; set; }
        public string RoomName { get; set; }

        public static GetRoomBookingDetailResponse ResponseFromEntity(RoomBookingRequestForm roomBooking, Student student, Entities.ParamEntities.Param roomType, Entities.ParamEntities.Param priorityType, Room room)
        {
            return new GetRoomBookingDetailResponse()
            {
                CreatedDate = roomBooking.CreatedDate.ToString(GlobalParams.DateTimeResponseFormat),
                StudentId = student.StudentId,
                RoomType = roomType.Name,
                LastUpdated = roomBooking.LastUpdated.ToString(GlobalParams.DateTimeResponseFormat),
                Term = student.Term,
                PriorityTypeId = priorityType.ParamId,
                Name = student.Name,
                StudentCardNumber = student.StudentCardNumber,
                BirthDay = student.BirthDay.ToString(GlobalParams.BirthDayFormat),
                Gender = student.Gender,
                Month = roomBooking.Month,
                PriorityType = priorityType.Name,
                PriorityImageUrl = roomBooking.PriorityImageUrl,
                PhoneNumber = student.PhoneNumber,
                IdentityCardImageUrl = roomBooking.IdentityCardImageUrl,
                StudentCardImageUrl = roomBooking.StudentCardImageUrl,
                Address = student.Address,
                RoomTypeId = roomBooking.TargetRoomType,
                IdentityCardNumber = student.IdentityNumber,
                RoomBookingId = roomBooking.RoomBookingRequestFormId,
                RoomId = room?.RoomId,
                RoomName = room?.Name,
                RestrictDate = roomBooking.RejectDate.ToString(GlobalParams.DateTimeResponseFormat)
            };
        }
    }
}