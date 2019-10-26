using System.Collections.Generic;
using System.Linq;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.ViewModels.RoomViewModels.ArrangeRoom
{
    public class ArrangeRoomResponse
    {
        public int StudentId { get; set; }
        public int RoomId { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string RoomName { get; set; }

        public List<ArrangeRoomResponse> ArrangeRoomListFromEntities(List<User> users, List<Student> students, List<Room> rooms)
        {
            //Go through every student and add new response to result list
            return (from student in students
                where student.RoomId != null
                select new ArrangeRoomResponse()
                {
                    RoomId = student.RoomId.Value, 
                    StudentId = student.StudentId, 
                    RoomName = rooms.Find(r => r.RoomId == student.RoomId).Name, 
                    StudentName = student.Name,
                    Email = users.Find(u=>u.UserId == student.StudentId).Email
                }).ToList();
        }
    }
}