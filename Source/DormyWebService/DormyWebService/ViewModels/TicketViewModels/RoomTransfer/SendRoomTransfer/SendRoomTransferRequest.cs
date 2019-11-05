using DormyWebService.Entities.TicketEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Utilities;

namespace DormyWebService.ViewModels.TicketViewModels.RoomTransfer.SendRoomTransfer
{
    public class SendRoomTransferRequest
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int TargetRoomType { get; set; }
        [Required]
        public string Description { get; set; }

        internal static RoomTransferRequestForm NewEntityFromRequest(SendRoomTransferRequest request)
        {
            return new RoomTransferRequestForm()
            {
                StudentId = request.StudentId,
                Reason = request.Description,
                TargetRoomType = request.TargetRoomType,
                CreatedDate = DateTime.Now.AddHours(GlobalParams.TimeZone),
                LastUpdated = DateTime.Now.AddHours(GlobalParams.TimeZone),
                RejectDate = DateTime.Now.AddHours(GlobalParams.TimeZone),
                Status = RequestStatus.Pending,
            };
        }
    }
}
