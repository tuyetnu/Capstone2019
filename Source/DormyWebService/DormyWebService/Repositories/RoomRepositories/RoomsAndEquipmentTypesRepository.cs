using System.Collections.Generic;
using System.Linq;
using DormyWebService.Entities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.ViewModels.RoomViewModels.GetAllMissingEquipmentRoom;
using Microsoft.EntityFrameworkCore;

namespace DormyWebService.Repositories.RoomRepositories
{
    public class RoomsAndEquipmentTypesRepository : RepositoryBase<RoomsAndEquipmentTypes>, IRoomsAndEquipmentTypesRepository
    {
        public RoomsAndEquipmentTypesRepository(DormyDbContext context) : base(context)
        {
        }

        public List<GetAllMissingEquipmentRoomResponse> FindAllAndIncludeByBuildingId(int buildingId)
        {
            var result = Context.RoomsAndEquipmentTypes
                .Include(r => r.Room)
                .Include(p => p.Param)
                .Where(b => b.Room.BuildingId == buildingId)
                .Select(r => GetAllMissingEquipmentRoomResponse.ResponseFromEntity(r))
                .ToList();
            return result;
        }
    }
}