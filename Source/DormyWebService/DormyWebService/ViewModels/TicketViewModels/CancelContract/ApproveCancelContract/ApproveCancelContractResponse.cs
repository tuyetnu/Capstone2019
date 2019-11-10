using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.TicketViewModels.CancelContract.ApproveCancelContract
{
    public class ApproveCancelContractResponse
    {
        public int CancelContractFormId;

        public ApproveCancelContractResponse(int cancelContractFormId)
        {
            CancelContractFormId = cancelContractFormId;
        }
    }
}
