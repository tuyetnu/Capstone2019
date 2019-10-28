using System.Collections.Generic;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.ViewModels.RoomViewModels.ArrangeRoom
{
    //Used to arrange room
    public class RoomAndStudentLists
    {
        public List<Student> Students { get; set; }

        public List<Room> Rooms { get; set; }

        public bool Gender { get; set; }

        public int RoomType { get; set; }

        public RoomAndStudentLists(bool gender, int roomType)
        {
            Gender = gender;
            RoomType = roomType;
        }
    }
}