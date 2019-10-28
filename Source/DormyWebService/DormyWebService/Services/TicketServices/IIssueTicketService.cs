using System.Collections.Generic;
using System.Threading.Tasks;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.ViewModels.TicketViewModels.IssueTicketViewModels.ChangeIssueTicketStatus;
using DormyWebService.ViewModels.TicketViewModels.IssueTicketViewModels.GetIssueTicket;
using DormyWebService.ViewModels.TicketViewModels.IssueTicketViewModels.SendIssueTicket;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.SendRoomBooking;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Services.TicketServices
{
    public interface IIssueTicketService
    {
        Task<IssueTicket> FindById(int id);

        Task<SendIssueTicketResponse> SendTicket(SendIssueTicketRequest request);

        Task<List<GetIssueTicketResponse>> AdvancedGetIssueTicket(string sorts, string filters, int? page,
            int? pageSize);

        Task<ChangeIssueTicketStatusResponse> ChangeIssueTicketStatus(ChangeIssueTicketStatusRequest request);
    }
}