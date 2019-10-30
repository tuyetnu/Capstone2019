using System.Collections.Generic;

namespace DormyWebService.ViewModels.TicketViewModels.RenewContractRequestViewModels.GetRenewContract
{
    public class AdvancedGetRenewContractResponse
    {
        public List<GetRenewContractResponse> ResultList { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
    }
}