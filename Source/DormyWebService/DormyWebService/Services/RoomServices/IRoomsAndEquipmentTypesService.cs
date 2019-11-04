using System.Threading.Tasks;
using DormyWebService.ViewModels.RoomViewModels.GetAllMissingEquipmentRoom;

namespace DormyWebService.Services.RoomServices
{
    public interface IRoomsAndEquipmentTypesService
    {
        Task<AdvancedGetAllMissingEquipmentRoomResponse> GetAllMissingEquipmentRoom(string sorts, string filters,
            int? page, int? pageSize);
    }
}