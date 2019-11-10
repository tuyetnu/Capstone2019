using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.TicketViewModels.CancelContract.RejectCancelContract
{
    public class RejectCancelContractRequest
    {
        public int CancelContractFormId { get; set; }
        public int StaffId { get; set; }
        public string Reason { get; set; }
    }
}
