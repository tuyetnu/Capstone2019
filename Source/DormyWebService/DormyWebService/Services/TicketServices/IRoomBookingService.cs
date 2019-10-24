using System.Threading.Tasks;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.ResolveRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.SendRoomBooking;

namespace DormyWebService.Services.TicketServices
{
    public interface IRoomBookingService
    {
        Task<RoomBookingRequestForm> FindById(int id);

        Task<SendRoomBookingResponse> SendRequest(SendRoomBookingRequest request);

        Task<ResolveRoomBookingResponse> ResolveRequest(ResolveRoomBookingRequest request);
    }
}