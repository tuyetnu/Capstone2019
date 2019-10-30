using DormyWebService.Entities;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.Repositories.RoomRepositories
{
    public class RoomGroupRepository : RepositoryBase<RoomGroup>, IRoomGroupRepository
    {
        public RoomGroupRepository(DormyDbContext context) : base(context)
        {
        }
    }
}