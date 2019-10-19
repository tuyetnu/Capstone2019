using DormyWebService.Entities;
using DormyWebService.Repositories.ParamRepositories;
using DormyWebService.Repositories.UserRepositories;

namespace DormyWebService.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DormyDbContext _context;
        private IUserRepository _user;
        private IStudentRepository _student;
        private IParamRepository _param;
        private IParamTypeRepository _paramType;

        public RepositoryWrapper(DormyDbContext context)
        {
            _context = context;
        }

        public IUserRepository User => _user ?? (_user = new UserRepository(_context));
        public IStudentRepository Student => _student ?? (_student = new StudentRepository(_context));
        public IParamRepository Param => _param ?? (_param = new ParamRepositories.ParamRepository(_context));
        public IParamTypeRepository ParamType => _paramType ?? (_paramType = new ParamTypeRepository(_context));
    }
}