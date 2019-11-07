using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Utilities;
using Sieve.Attributes;

namespace DormyWebService.ViewModels.TicketViewModels.RoomTransfer.GetRoomTransfer
{
    public class GetRoomTransferResponse
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int RoomTransferRequestFormId { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string CreatedDate { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string LastUpdated { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string RejectDate { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string TransferDate { get; set; }
        public string Reason { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int StudentId { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string StudentName { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string StudentCardNumber { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string StudentEmail { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int TargetRoomType { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string TargetRoomTypeName { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string CurrentRoomTypeName { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int RoomId { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string RoomName { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Status { get; set; }

        public static GetRoomTransferResponse ResponseFromEntity(RoomTransferRequestForm roomTransfer, Student student, Room room, Entities.ParamEntities.Param roomType)
        {
            var currentRoomType = "Standard Room";
            if (roomTransfer.Room.RoomType == 12)
            {
                currentRoomType = "Service Room";
            }
            return new GetRoomTransferResponse()
            {
                StudentId = roomTransfer.StudentId,
                StudentName = student.Name,
                StudentCardNumber = student.StudentCardNumber,
                StudentEmail = student.Email,
                RoomId = roomTransfer.RoomId ?? -1,
                RoomName = room?.Name ?? "null",
                CurrentRoomTypeName = currentRoomType,
                Status = roomTransfer.Status,
                CreatedDate = roomTransfer.CreatedDate.ToString(GlobalParams.DateTimeResponseFormat),
                LastUpdated = roomTransfer.LastUpdated.ToString(GlobalParams.DateTimeResponseFormat),
                RejectDate = roomTransfer.RejectDate.ToString(GlobalParams.DateTimeResponseFormat),
                TransferDate = roomTransfer.TransferDate.ToString(GlobalParams.DateTimeResponseFormat),
                TargetRoomType = roomTransfer.TargetRoomType,
                TargetRoomTypeName = roomType.Name,
                Reason = roomTransfer.Reason,
                RoomTransferRequestFormId = roomTransfer.RoomTransferRequestFormId
            };
        }
    }
}