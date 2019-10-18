using DormyWebService.Entities;
using DormyWebService.Entities.AccountEntities;

namespace DormyWebService.Repositories.UserRepositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DormyDbContext context) : base(context)
        {
        }
    }
}