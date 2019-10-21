using System.Threading.Tasks;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.Services.RoomServices
{
    public interface IRoomService
    {
        Task<Room> FindById(int id);
    }
}