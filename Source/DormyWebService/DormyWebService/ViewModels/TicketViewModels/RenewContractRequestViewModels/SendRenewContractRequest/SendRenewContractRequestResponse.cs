using DormyWebService.Entities.TicketEntities;

namespace DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.SendRenewContractRequest
{
    public class SendRenewContractRequestResponse
    {
        public int ContractRenewalFormId { get; set; }

        public static SendRenewContractRequestResponse ResponseFromEntity(ContractRenewalForm contractRenewalForm)
        {
            return new SendRenewContractRequestResponse()
            {
                ContractRenewalFormId = contractRenewalForm.ContractRenewalFormId
            };
        }
    }

   
}