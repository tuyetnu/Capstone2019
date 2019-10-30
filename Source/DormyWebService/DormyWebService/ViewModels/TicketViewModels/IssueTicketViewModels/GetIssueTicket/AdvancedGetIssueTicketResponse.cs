using System.Collections.Generic;

namespace DormyWebService.ViewModels.TicketViewModels.IssueTicketViewModels.GetIssueTicket
{
    public class AdvancedGetIssueTicketResponse
    {
        public List<GetIssueTicketResponse> ResultList { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
    }
}