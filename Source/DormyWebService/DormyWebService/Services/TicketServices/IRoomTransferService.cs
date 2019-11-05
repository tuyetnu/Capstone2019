using DormyWebService.Entities.TicketEntities;
using DormyWebService.ViewModels.TicketViewModels.RoomTransfer.SendRoomTransfer;
using System.Threading.Tasks;
using DormyWebService.ViewModels.TicketViewModels.RoomTransfer.ApproveRoomTransfer;
using DormyWebService.ViewModels.TicketViewModels.RoomTransfer.GetRoomTransfer;

namespace DormyWebService.Services.TicketServices
{
    public interface IRoomTransferService
    {
        Task<RoomTransferRequestForm> FindById(int id);
        Task<AdvancedGetRoomTransferResponse> AdvancedGetRoomTransfer(string sorts, string filters, int? page, int? pageSize);
        Task<SendRoomTransferRespone> SendRequest(SendRoomTransferRequest request);
        //Task<bool> EditRoomRequest(EditRoomTransferRequest request);
        Task<ApproveRoomTransferResponse> ApproveRoomTransfer(int id);
        Task<bool> AutoRejectRoomTransfer();
        Task<bool> AutoCompleteRoomTransfer();
    }
}