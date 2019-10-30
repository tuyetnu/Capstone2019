using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.ContractEntities;
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
        public string Status { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int Month { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string CreatedDate { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string LastUpdated { get; set; }

        public static GetRenewContractResponse ResponseFromEntity(ContractRenewalForm form, Student student, Staff staff)
        {
            return new GetRenewContractResponse()
            {
                StudentId = form.StudentId,
                Status = form.Status,
                CreatedDate = form.CreatedDate.ToString(GlobalParams.DateTimeResponseFormat),
                LastUpdated = form.LastUpdated.ToString(GlobalParams.DateTimeResponseFormat),
                StaffId = staff?.StaffId ?? -1,
                StaffName = staff?.Name ?? "null",
                Month = form.Month,
                StudentName = student.Name,
                ContractId = form.ContractId,
                ContractRenewalFormId = form.ContractRenewalFormId,
                StudentEmail = student.Email
            };
        }
    }
}