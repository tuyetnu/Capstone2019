using DormyWebService.Entities.TicketEntities;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.ResolveRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomTransfer.SendRoomTransfer;
using DormyWebService.ViewModels.TicketViewModels.RoomTransferRequestViewModels.ResolveRoomTransferRequest;
using System.Threading.Tasks;

namespace DormyWebService.Services.TicketServices
{
    public interface IRoomTransferService
    {
        Task<RoomTransferRequestForm> FindById(int id);
        Task<SendRoomTransferRespone> SendRequest(SendRoomTransferRequest request);
        Task<bool> DeleteRoomTransfer(int id);
        //Task<bool> EditRoomRequest(EditRoomTransferRequest request);

        Task<ResolveRoomTransferResponse> ResolveRequest(ResolveRoomTransferRequest request);


    }
}