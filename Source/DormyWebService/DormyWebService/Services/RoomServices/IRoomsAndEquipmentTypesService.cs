using System.Threading.Tasks;
using DormyWebService.ViewModels.RoomViewModels.GetAllMissingEquipmentRoom;
using Sieve.Models;

namespace DormyWebService.Services.RoomServices
{
    public interface IRoomsAndEquipmentTypesService
    {
        AdvancedGetAllMissingEquipmentRoomResponse GetAllMissingEquipmentRoomByBuildingId(SieveModel sieveModel, int buildingId);
    }
}