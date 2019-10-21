using System.Threading.Tasks;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.ViewModels.RoomViewModels;

namespace DormyWebService.Services.RoomServices
{
    public interface IRoomService
    {
        Task<Room> FindById(int id);
        Task<CreateRoomResponse> CreateRoom(CreateRoomRequest requestModel);
    }
}