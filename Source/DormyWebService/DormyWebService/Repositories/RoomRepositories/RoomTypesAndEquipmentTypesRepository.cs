using DormyWebService.Entities;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.Repositories.RoomRepositories
{
    public class RoomTypesAndEquipmentTypesRepository : RepositoryBase<RoomTypesAndEquipmentTypes>, IRoomTypesAndEquipmentTypesRepository
    {
        public RoomTypesAndEquipmentTypesRepository(DormyDbContext context) : base(context)
        {
        }
    }
}