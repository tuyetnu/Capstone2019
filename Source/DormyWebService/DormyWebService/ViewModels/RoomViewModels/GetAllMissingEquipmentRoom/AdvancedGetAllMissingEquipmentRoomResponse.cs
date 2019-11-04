using System.Collections.Generic;

namespace DormyWebService.ViewModels.RoomViewModels.GetAllMissingEquipmentRoom
{
    public class AdvancedGetAllMissingEquipmentRoomResponse
    {
        public List<GetAllMissingEquipmentRoomResponse> ResultList { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
    }
}