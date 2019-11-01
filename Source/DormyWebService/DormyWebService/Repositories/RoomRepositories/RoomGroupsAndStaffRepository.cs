using DormyWebService.Entities;
using DormyWebService.Entities.RoomEntities;

namespace DormyWebService.Repositories.RoomRepositories
{
    public class RoomGroupsAndStaffRepository : RepositoryBase<RoomGroupsAndStaff>, IRoomGroupsAndStaffRepository
    {
        public RoomGroupsAndStaffRepository(DormyDbContext context) : base(context)
        {
        }
    }
}