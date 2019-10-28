using DormyWebService.ViewModels.TicketViewModels.RoomTransfer.SendRoomTransfer;
using System.Threading.Tasks;

namespace DormyWebService.Services.TicketServices
{
    public interface IRoomTransferService
    {
        Task<SendRoomTransferRespone> SendRequest(SendRoomTransferRequest request);
    }
}