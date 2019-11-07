using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.TicketViewModels.RoomTransfer.GetRoomTransferDetail
{
    public class RoomTransferDetailResponse
    {
        public int RoomTransferRequestFormId { get; set; }
        public string CreatDate { get; set; }

        public string LastUpdated { get; set; }
        public string RejectDate { get; set; }
        public string Reason { get; set; }
        public string TargetRoomType { get; set; }
        public int TargetRoomTypeId { get; set; }
        public string Status { get; set; }
        public string TransferDate { get; set; }

        public RoomTransferDetailResponse(RoomTransferRequestForm roomTransfer, Entities.ParamEntities.Param roomType)
        {
            RoomTransferRequestFormId = roomTransfer.RoomTransferRequestFormId;
            CreatDate = roomTransfer.CreatedDate.ToString(GlobalParams.DateTimeResponseFormat);
            LastUpdated = roomTransfer.LastUpdated.ToString(GlobalParams.DateTimeResponseFormat);
            RejectDate = roomTransfer.RejectDate.ToString(GlobalParams.DateTimeResponseFormat);
            Reason = roomTransfer.Reason;
            TargetRoomType = roomType.Name;
            TargetRoomTypeId = roomTransfer.TargetRoomType;
            Status = roomTransfer.Status;
            TransferDate = roomTransfer.TransferDate.ToString(GlobalParams.DateTimeResponseFormat);
        }
    }
}
