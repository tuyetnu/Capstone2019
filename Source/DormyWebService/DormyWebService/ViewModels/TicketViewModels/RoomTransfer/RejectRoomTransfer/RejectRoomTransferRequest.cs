using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.TicketViewModels.RoomTransfer.RejectRoomTransfer
{
    public class RejectRoomTransferRequest
    {
        [Required]
        public int RoomTransferId { get; set; }

        [Required]
        public string Reason { get; set; }
    }
}
