using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Utilities;

namespace DormyWebService.ViewModels.TicketViewModels.RoomBooking.GetRoomBookingDetail
{
    public class GetRoomBookingDetailResponse
    {
        public int StudentId { get; set; }
        public string IdentityCardNumber { get; set; }
        public bool Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int Term { get; set; }
        public string PriorityTypeId { get; set; }
        public string PriorityType { get; set; }
        public string PriorityImageUrl { get; set; }
        public string IdentityCardImageUrl { get; set; }
        public string StudentCardImageUrl { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomType { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdated { get; set; }

        public static GetRoomBookingDetailResponse ResponseFromEntity(RoomBookingRequestForm roomBooking, Student student)
        {
            return new GetRoomBookingDetailResponse()
            {
                CreatedDate = roomBooking.CreatedDate.ToString(GlobalParams.DateTimeResponseFormat),
                StudentId = student.StudentId,
//                RoomType = roomType.Name,
                LastUpdated = roomBooking.LastUpdated.ToString(GlobalParams.DateTimeResponseFormat),
                Term = student.Term,
//                PriorityType = student.PriorityType,
                
            };
        }
    }
}