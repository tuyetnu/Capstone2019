using System.Collections.Generic;
using System.Linq;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.ViewModels.RoomViewModels.ArrangeRoom
{
    public class ArrangeRoomResponse
    {
        public List<ArrangeRoomResponseStudent> ArrangedStudents { get; set; }

        public List<ArrangeRoomResponse> ArrangeRoomListFromEntities(List<User> users, List<Student> students, List<Room> rooms)
        {
        }
    }
}