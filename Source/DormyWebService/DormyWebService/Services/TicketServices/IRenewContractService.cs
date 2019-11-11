using System.Threading.Tasks;
using DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.ApproveRenewContract;
using DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.GetRenewContract;
using DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.GetRenewContractDetail;
using DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.RejectRenewContract;
using DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.SendRenewContractRequest;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Services.TicketServices
{
    public interface IRenewContractService
    {
        Task<SendRenewContractRequestResponse> SendRenewContract(SendRenewContractRequestRequest request);
        Task<AdvancedGetRenewContractResponse> AdvancedGetRenewContract(string sorts, string filters, int? page, int? pageSize);
        Task<ActionResult<ApproveRenewContractResponse>> ApproveContractRenewal(ApproveRenewContractRequest approveRenew);
        Task<ActionResult<RejectRenewContractResponse>> RejectContractRenewal(RejectRenewContractRequest rejectRenew);
        Task<ActionResult<RenewContractDetailResponse>> GetRenewContractDetail(int id);
    }
}