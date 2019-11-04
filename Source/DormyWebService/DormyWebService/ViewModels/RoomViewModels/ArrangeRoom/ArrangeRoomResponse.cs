using System.Collections.Generic;
using System.Linq;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.ImportRoomBooking;

namespace DormyWebService.ViewModels.RoomViewModels.ArrangeRoom
{
    public class ArrangeRoomResponse
    {
        public List<ArrangeRoomResponseStudent> ArrangedStudents { get; set; }
        public List<ArrangeRoomResponseUnArrangedStudent> UnArrangedStudents { get; set; }


        public static ArrangeRoomResponse ArrangeRoomListFromEntities(List<ImportStudentAndRequest> arrangedStudents, List<Student> unArrangedStudents, List<Room> rooms)
        {
            var result = new ArrangeRoomResponse();

//            for (var i = 0; i < arrangedStudents.Count; i++)
//            {
//                System.Diagnostics.Debug.WriteLine("StudentId: " + arrangedStudents[i].StudentId);
//                System.Diagnostics.Debug.WriteLine("Name: " + arrangedStudents[i].Name);
//                System.Diagnostics.Debug.WriteLine("Email " + arrangedStudents[i].Email);
//                System.Diagnostics.Debug.WriteLine("RoomId " + arrangedStudents[i].RoomId);
//                System.Diagnostics.Debug.WriteLine("RoomName " + rooms.Find(r => arrangedStudents[i].RoomId != null && r.RoomId == arrangedStudents[i].RoomId.Value).Name);
//
//            }

            result.ArrangedStudents = arrangedStudents.Select(s => new ArrangeRoomResponseStudent()
            {
                StudentId = s.Student.StudentId,
                Email = s.Student.Email,
                RoomId = s.Student.RoomId ?? -1,
                RoomName = rooms.Find(r => s.Student.RoomId != null && r.RoomId == s.Student.RoomId.Value).Name,
                StudentName = s.Student.Name,
                RoomBookingId = s.RoomBooking.RoomBookingRequestFormId
            }).ToList();

            result.UnArrangedStudents = unArrangedStudents.Select(s => new ArrangeRoomResponseUnArrangedStudent()
                {
                    StudentId = s.StudentId,
                    Email = s.Email,
                    StudentName = s.Name
                }).ToList();

            return result;
        }
    }
}