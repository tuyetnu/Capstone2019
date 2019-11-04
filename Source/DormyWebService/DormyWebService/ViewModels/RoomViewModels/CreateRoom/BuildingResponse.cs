using System.Collections.Generic;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Utilities;

namespace DormyWebService.ViewModels.RoomViewModels.CreateRoom
{
    public class BuildingResponse
    {
        public int BuildingId { get; set; }
        public string Name { get; set; }
        public int NumberOfFloor { get; set; }
        public int RoomOnEachFloor { get; set; }
    }
}