using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.ContractEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Utilities;
using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.TicketViewModels.CancelContract.GetCancelContract
{
    public class CancelContractResponse
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int CancelContractFormId { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int StudentId { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string StudentName { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string StudentEmail { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int? StaffId { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string StaffName { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int ContractId { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Status { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string CreatedDate { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string LastUpdated { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string CancelationDate { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int RoomId { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string RoomName { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string StudentCode { get; set; }

        public static CancelContractResponse ResponseFromEntity(CancelContractForm form, Student student, Staff staff, Contract contract, Room room)
        {
            return new CancelContractResponse()
            {
                StudentId = form.StudentId,
                Status = form.Status,
                CreatedDate = form.CreatedDate.ToString(GlobalParams.DateTimeResponseFormat),
                LastUpdated = form.LastUpdated.ToString(GlobalParams.DateTimeResponseFormat),
                CancelationDate = form.CancelationDate.ToString(GlobalParams.DateTimeResponseFormat),
                StaffId = staff?.StaffId ?? -1,
                StaffName = staff?.Name ?? "null",
                StudentName = student.Name,
                StudentCode = student.StudentCardNumber,
                ContractId = contract.ContractId,
                CancelContractFormId = form.CancelContractFormId,
                StudentEmail = student.Email,
                RoomId = room.RoomId,
                RoomName = room.Name
            };
        }
    }
}
