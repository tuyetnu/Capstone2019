using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.TicketViewModels.CancelContract.RejectCancelContract
{
    public class RejectCancelContractRespone
    {
        public int RejectContractFormId;

        public RejectCancelContractRespone(int cancelContractFormId)
        {
            RejectContractFormId = cancelContractFormId;
        }
    }
}
