using DormyWebService.Entities;
using DormyWebService.Entities.AccountEntities;

namespace DormyWebService.Repositories.StaffRepositories
{
    public class StaffRepository : RepositoryBase<Staff>, IStaffRepository
    {
        public StaffRepository(DormyDbContext context) : base(context)
        {
        }
    }
}