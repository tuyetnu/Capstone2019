using System.Collections.Generic;
using System.Threading.Tasks;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.ViewModels.IssueTicketViewModels.GetIssueTicket;
using DormyWebService.ViewModels.IssueTicketViewModels.SendIssueTicket;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.SendRoomBooking;

namespace DormyWebService.Services.TicketServices
{
    public interface IIssueTicketService
    {
        Task<IssueTicket> FindById(int id);

        Task<SendIssueTicketReponse> SendTicket(SendIssueTicketRequest request);

        Task<List<GetIssueTicketResponse>> AdvancedGetIssueTicket(string sorts, string filters, int? page,
            int? pageSize);
    }
}