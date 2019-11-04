using System.Collections.Generic;
using System.Linq;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Entities.TicketEntities;

namespace DormyWebService.ViewModels.RoomViewModels.ArrangeRoom
{
    public class ArrangeRoomResponseStudent
    {
        public int RoomBookingId { get; set; }
        public int StudentId { get; set; }
        public int RoomId { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string RoomName { get; set; }

        public static ArrangeRoomResponseStudent ResponseFromEntity(Student student, Room room, RoomBookingRequestForm roomBooking)
        {
            return new ArrangeRoomResponseStudent()
            {
                StudentId = student.StudentId,
                RoomId = room.RoomId,
                Email = student.Email,
                RoomName = room.Name,
                StudentName = student.Name,
                RoomBookingId = roomBooking.RoomBookingRequestFormId
            };
        }
    }
}