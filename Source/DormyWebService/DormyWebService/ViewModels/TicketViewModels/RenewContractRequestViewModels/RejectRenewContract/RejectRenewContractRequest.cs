using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.RejectRenewContract
{
    public class RejectRenewContractRequest
    {
        public int RenewContractFormId { get; set; }
        public int StaffId { get; set; }
        public string Reason { get; set; }
    }
}
