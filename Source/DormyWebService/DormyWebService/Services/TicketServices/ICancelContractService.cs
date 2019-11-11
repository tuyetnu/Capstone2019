using System.Threading.Tasks;
using DormyWebService.ViewModels.TicketViewModels.CancelContract;
using DormyWebService.ViewModels.TicketViewModels.CancelContract.ApproveCancelContract;
using DormyWebService.ViewModels.TicketViewModels.CancelContract.GetCancelContract;
using DormyWebService.ViewModels.TicketViewModels.CancelContract.RejectCancelContract;
using DormyWebService.ViewModels.TicketViewModels.CancelContract.SendCancelContractRequest;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Services.TicketServices
{
    public interface ICancelContractService
    {
        
        Task<ActionResult<SendCancelContractFormResponse>> SendCancelContract(SendCancelContractFormRequest request);
        Task<ActionResult<AdvancedGetCancelContractResponse>> AdvancedGetCancelContract(string sorts, string filters, int? page, int? pageSize);
        Task<ActionResult<ApproveCancelContractResponse>> ApproveContractCancel(ApproveCancelContractRequest approveCancel);
        Task<ActionResult<RejectCancelContractRespone>> RejectCancelContract(RejectCancelContractRequest rejectCancel);
        Task<ActionResult<GetCancelContractDetail>> GetCancelContractDetail(int id);
    }
}