using DormyWebService.Entities.TicketEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.ViewModels.TicketViewModels.CancelContract.SendCancelContractRequest
{
    public class SendCancelContractFormResponse
    {
        public int ContractCancellFormId { get; set; }

        public static SendCancelContractFormResponse ResponseFromEntity(CancelContractForm cancelContractForm)
        {
            return new SendCancelContractFormResponse()
            {
                ContractCancellFormId = cancelContractForm.CancelContractFormId,
            };
        }
    }
}
