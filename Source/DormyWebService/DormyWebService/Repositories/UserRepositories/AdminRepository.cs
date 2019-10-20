using DormyWebService.Entities;
using DormyWebService.Entities.AccountEntities;

namespace DormyWebService.Repositories.UserRepositories
{
    public class AdminRepository : RepositoryBase<Admin>, IAdminRepository
    {
        public AdminRepository(DormyDbContext context) : base(context)
        {
        }
    }
}