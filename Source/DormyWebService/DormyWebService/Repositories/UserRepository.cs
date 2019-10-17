using System.Collections.Generic;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;

namespace DormyWebService.Repositories
{
    public class UserRepository : IRepository<User>, IUserRepository
    {
        public Task<List<User>> FindAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<User> FindById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateAsync(User entity)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(User entity)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(User entity)
        {
            throw new System.NotImplementedException();
        }
    }
}