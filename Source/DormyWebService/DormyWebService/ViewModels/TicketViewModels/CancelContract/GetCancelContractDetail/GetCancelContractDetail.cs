using DormyWebService.Entities.TicketEntities;
using DormyWebService.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.TicketViewModels.CancelContract
{
    public class GetCancelContractDetail
    {
        public int CancelContractFormId { get; set; }
        public int StudentId { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
        public string CreateDate { get; set; }
        public string LastUpdate { get; set; }
        public string CancelationDate { get; set; }
        public int ContractId { get; set; }

        public GetCancelContractDetail(CancelContractForm cancelContractForm)
        {
            CancelContractFormId = cancelContractForm.CancelContractFormId;
            StudentId = cancelContractForm.StudentId;
            StaffId = cancelContractForm.Staff.StaffId;
            StaffName = cancelContractForm.Staff.Name;
            Status = cancelContractForm.Status;
            Reason = cancelContractForm.Reason;
            Description = cancelContractForm.Description;
            CreateDate = cancelContractForm.CreatedDate.ToString(GlobalParams.DateTimeResponseFormat);
            LastUpdate = cancelContractForm.LastUpdated.ToString(GlobalParams.DateTimeResponseFormat);
            CancelationDate = cancelContractForm.CancelationDate.ToString(GlobalParams.DateTimeResponseFormat);
            ContractId = cancelContractForm.ContractId;
        }
    }
}
