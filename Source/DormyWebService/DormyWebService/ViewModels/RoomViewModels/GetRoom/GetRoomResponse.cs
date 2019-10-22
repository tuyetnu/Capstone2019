using System.Collections.Generic;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Utilities;

namespace DormyWebService.ViewModels.RoomViewModels.GetRoom
{
    public class GetRoomResponse
    {
        public int RoomId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> EquipmentIds { get; set; }
        public int Capacity { get; set; }
        public int RoomType { get; set; }
        public decimal Price { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdated { get; set; }
        public string RoomStatus { get; set; }

        public static GetRoomResponse ResponseFromRoom(Room room, List<int> equipmentIds, List<Student> students)
        {
            return new GetRoomResponse()
            {
                RoomId = room.RoomId,
                Name = room.Name,
                RoomType = room.RoomType,
                Price = room.Price,
                EquipmentIds = equipmentIds,
                Description = room.Description,
                Capacity = room.Capacity,
                CreatedDate = room.CreatedDate.ToString(GlobalParams.DateTimeResponseFormat),
                LastUpdated = room.LastUpdated.ToString(GlobalParams.DateTimeResponseFormat),
                RoomStatus = room.RoomStatus
            };
        }
    }
}