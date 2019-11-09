using System.Threading.Tasks;
using DormyWebService.ViewModels.TicketViewModels.CancelContract.SendCancelContractRequest;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Services.TicketServices
{
    public interface ICancelContractService
    {
        Task<ActionResult<SendCancelContractFormResponse>> SendRenewContract(SendCancelContractFormRequest request);
    }
}