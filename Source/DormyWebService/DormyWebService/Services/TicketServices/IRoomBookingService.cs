using System.Threading.Tasks;
using DormyWebService.Entities.TicketEntities;

namespace DormyWebService.Services.TicketServices
{
    public interface IRoomBookingService
    {
        Task<RoomBookingRequestForm> FindById(int id);
    }
}