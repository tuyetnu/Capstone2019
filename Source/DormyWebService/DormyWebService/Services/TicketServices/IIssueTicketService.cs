using System.Collections.Generic;
using System.Threading.Tasks;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.ViewModels.TicketViewModels.IssueTicketViewModels.ChangeIssueTicketStatus;
using DormyWebService.ViewModels.TicketViewModels.IssueTicketViewModels.EditIssueTicket;
using DormyWebService.ViewModels.TicketViewModels.IssueTicketViewModels.GetIssueTicket;
using DormyWebService.ViewModels.TicketViewModels.IssueTicketViewModels.GetIssueTicketDetail;
using DormyWebService.ViewModels.TicketViewModels.IssueTicketViewModels.SendIssueTicket;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.EditRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.SendRoomBooking;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Services.TicketServices
{
    public interface IIssueTicketService
    {
        Task<IssueTicket> FindById(int id);
        Task<GetIssueTicketDetailResponse> GetIssueTicketDetail(int id);
        Task<List<GetIssueTicketResponse>> GetByStudent(int id);
        Task<AdvancedGetIssueTicketResponse> AdvancedGetIssueTicket(string sorts, string filters, int? page,
            int? pageSize);
        Task<SendIssueTicketResponse> SendTicket(SendIssueTicketRequest request);

        Task<bool> EditIssueTicket(EditIssueTicketRequest request);

        

        Task<ChangeIssueTicketStatusResponse> ChangeIssueTicketStatus(ChangeIssueTicketStatusRequest request);
    }
}