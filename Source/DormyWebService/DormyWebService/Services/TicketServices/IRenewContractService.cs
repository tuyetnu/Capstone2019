using System.Threading.Tasks;
using DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.SendRenewContractRequest;

namespace DormyWebService.Services.TicketServices
{
    public interface IRenewContractService
    {
        Task<SendRenewContractRequestResponse> SendRenewContract(SendRenewContractRequestRequest request);
    }
}