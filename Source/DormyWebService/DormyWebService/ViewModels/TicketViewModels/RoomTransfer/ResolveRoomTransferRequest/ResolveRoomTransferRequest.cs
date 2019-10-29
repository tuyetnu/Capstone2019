using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.TicketViewModels.RoomTransferRequestViewModels.ResolveRoomTransferRequest
{
    public class ResolveRoomTransferRequest
    {

        public int RoomTransferRequestFormId { get; set; }
        public string Status { get; set; }
        public int StaffId { get; set; }
        public string Reason { get; set; }
    }
}
