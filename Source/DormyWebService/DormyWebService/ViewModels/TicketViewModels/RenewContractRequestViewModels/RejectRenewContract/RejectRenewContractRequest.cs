using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.RejectRenewContract
{
    public class RejectRenewContractRequest
    {
        public int contractId { get; set; }
        public int staffId { get; set; }
        public string reason { get; set; }
    }
}
