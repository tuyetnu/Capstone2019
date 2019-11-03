using DormyWebService.Entities;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.Repositories.RoomRepositories
{
    public class RoomsAndEquipmentTypesRepository : RepositoryBase<RoomsAndEquipmentTypes>, IRoomsAndEquipmentTypesRepository
    {
        public RoomsAndEquipmentTypesRepository(DormyDbContext context) : base(context)
        {
        }
    }
}