using DormyWebService.Entities.TicketEntities;
using DormyWebService.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.GetRenewContractDetail
{
    public class RenewContractDetailResponse
    {
        public int RenewContractFormId { get; set; }
        public int StudentId { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public string Status { get; set; }
        public int Month { get; set; }
        public string CreateDate { get; set; }
        public string LastUpdate { get; set; }
        public string Reason { get; set; }
        

        public RenewContractDetailResponse(ContractRenewalForm renewContract)
        {
            RenewContractFormId = renewContract.ContractRenewalFormId;
            StudentId = renewContract.StudentId;
            StaffId = renewContract.Staff.StaffId;
            StaffName = renewContract.Staff.Name;
            Status = renewContract.Status;
            Month = renewContract.Month;
            CreateDate = renewContract.CreatedDate.ToString(GlobalParams.DateTimeResponseFormat);
            LastUpdate = renewContract.LastUpdated.ToString(GlobalParams.DateTimeResponseFormat);
            Reason = renewContract.Reason;
        }
    }
}
