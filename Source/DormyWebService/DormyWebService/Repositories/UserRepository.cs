using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DormyWebService.Entities;
using DormyWebService.Entities.AccountEntities;

namespace DormyWebService.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DormyDbContext context) : base(context)
        {
        }
    }
}