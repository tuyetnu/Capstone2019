using System.Collections.Generic;
using System.Linq;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.ViewModels.RoomViewModels.ArrangeRoom
{
    public class ArrangeRoomResponseStudent
    {
        public int StudentId { get; set; }
        public int RoomId { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string RoomName { get; set; }
    }
}