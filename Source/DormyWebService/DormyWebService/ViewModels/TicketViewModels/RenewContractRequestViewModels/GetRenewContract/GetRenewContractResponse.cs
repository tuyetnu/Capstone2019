using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.ContractEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Utilities;
using Sieve.Attributes;

namespace DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.GetRenewContract
{
    public class GetRenewContractResponse
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int ContractRenewalFormId { get; set; }

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
        public DateTime DateEndContract { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Status { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int Month { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string CreatedDate { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string LastUpdated { get; set; }

        public string RoomName { get; set; }
        public int RoomType { get; set; }
        public int RoomId { get; set; }
        public string StudentCode { get; set; }

        public static GetRenewContractResponse ResponseFromEntity(ContractRenewalForm form)
        {
            return new GetRenewContractResponse()
            {
                StudentId = form.StudentId,
                Status = form.Status,
                CreatedDate = form.CreatedDate.ToString(GlobalParams.DateTimeResponseFormat),
                LastUpdated = form.LastUpdated.ToString(GlobalParams.DateTimeResponseFormat),
                StaffId = form.Staff?.StaffId ?? -1,
                StaffName = form.Staff?.Name ?? "null",
                Month = form.Month,
                StudentName = form.Student.Name,
                ContractId = form.ContractId,
                ContractRenewalFormId = form.ContractRenewalFormId,
                StudentEmail = form.Student.Email,
                StudentCode = form.Student.StudentCardNumber,
                RoomName = form.Student.Room.Name,
                RoomId = form.Student.Room.RoomId,
                DateEndContract = form.Contract.EndDate,
                RoomType = form.Student.Room.RoomType

            };
        }
    }
}