using DormyWebService.Entities.AccountEntities;
using DormyWebService.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.RoomViewModels.GetRoomInfoOfAStudent
{
    public class StudentRoomInfoResponse
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string RoomName { get; set; }
        public int RoomType { get; set; }
        public List<Roomate> Roomates { get; set; }

        public StudentRoomInfoResponse(Student student, List<Roomate> roomates)
        {
            StudentId = student.StudentId;
            StudentName = student.Name;
            RoomName = student.Room.Name;
            RoomType = student.Room.RoomType;
            Roomates = roomates;
        }
    }

    public class Roomate
    {
        public int RoomateId { get; set; }
        public string RoomateName { get; set; }
        public string RoomatePhoneNumber { get; set; }

        public Roomate(Student student)
        {
            RoomateId = student.StudentId;
            RoomateName = student.Name;
            RoomatePhoneNumber = student.PhoneNumber;
        }
    }
}
