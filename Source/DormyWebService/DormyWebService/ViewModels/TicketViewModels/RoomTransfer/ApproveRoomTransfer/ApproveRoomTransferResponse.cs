using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Entities.TicketEntities;

namespace DormyWebService.ViewModels.TicketViewModels.RoomTransfer.ApproveRoomTransfer
{
    public class ApproveRoomTransferResponse
    {
        public int RoomTransferRequestFormId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string Status { get; set; }

        public static ApproveRoomTransferResponse ResponseFromEntity(RoomTransferRequestForm roomTransfer, Student student, Room room)
        {
            return new ApproveRoomTransferResponse()
            {
                StudentId = roomTransfer.StudentId,
                RoomId = room.RoomId,
                Status = roomTransfer.Status,
                RoomName = room.Name,
                StudentName = student.Name,
                RoomTransferRequestFormId = roomTransfer.RoomTransferRequestFormId
            };
        }
    }
}