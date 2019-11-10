using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.TicketViewModels.CancelContract.ApproveCancelContract
{
    public class ApproveCancelContractRequest
    {
        public int cancelContractFormId { get; set; }
        public int staffId { get; set; }
    }
}
