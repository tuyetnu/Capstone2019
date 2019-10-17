using DormyWebService.Entities;

namespace DormyWebService.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DormyDbContext _context;
        private IUserRepository _user;

        public RepositoryWrapper(DormyDbContext context)
        {
            _context = context;
        }

        public IUserRepository User => _user ?? (_user = new UserRepository(_context));
    }
}