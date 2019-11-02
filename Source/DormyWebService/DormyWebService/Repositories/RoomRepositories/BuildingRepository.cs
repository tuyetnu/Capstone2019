using DormyWebService.Entities;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.Repositories.RoomRepositories
{
    public class BuildingRepository : RepositoryBase<Building>, IBuildingRepository
    {
        public BuildingRepository(DormyDbContext context) : base(context)
        {
        }
    }
}