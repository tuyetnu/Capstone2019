using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.RejectRenewContract
{
    public class RejectRenewContractResponse
    {
        public int RenewContractFormId;

        public RejectRenewContractResponse(int renewContractFormId)
        {
            RenewContractFormId = renewContractFormId;
        }
    }
}
