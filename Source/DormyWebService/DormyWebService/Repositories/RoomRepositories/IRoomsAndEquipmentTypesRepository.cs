using DormyWebService.Entities.RoomEntities;
using DormyWebService.ViewModels.RoomViewModels.GetAllMissingEquipmentRoom;
using System.Collections.Generic;

namespace DormyWebService.Repositories.RoomRepositories
{
    public interface IRoomsAndEquipmentTypesRepository : IRepository<RoomsAndEquipmentTypes>
    {
        List<GetAllMissingEquipmentRoomResponse> FindAllAndIncludeByBuildingId(int buildingId);
    }
}