using DormyWebService.Entities;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.Repositories.RoomRepositories
{
    public class RoomDivisionRepository : RepositoryBase<RoomDivision>, IRoomDivisionRepository
    {
        public RoomDivisionRepository(DormyDbContext context) : base(context)
        {
        }
    }
}