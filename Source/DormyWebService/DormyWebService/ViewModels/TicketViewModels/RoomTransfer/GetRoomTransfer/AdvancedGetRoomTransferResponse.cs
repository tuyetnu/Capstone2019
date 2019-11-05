using System.Collections.Generic;

namespace DormyWebService.ViewModels.TicketViewModels.RoomTransfer.GetRoomTransfer
{
    public class AdvancedGetRoomTransferResponse
    {
        public List<GetRoomTransferResponse> ResultList { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
    }
}