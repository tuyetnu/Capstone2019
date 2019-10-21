using DormyWebService.Entities;
using DormyWebService.Repositories.EquipmentRepository;
using DormyWebService.Repositories.NewsRepositories;
using DormyWebService.Repositories.ParamRepositories;
using DormyWebService.Repositories.RoomRepositories;
using DormyWebService.Repositories.UserRepositories;

namespace DormyWebService.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly DormyDbContext _context;
        private IUserRepository _user;
        private IStudentRepository _student;
        private IAdminRepository _admin;
        private INewsRepository _news;
        private IParamRepository _param;
        private IParamTypeRepository _paramType;
        private IEquipmentRepository _equipment;
        private IRoomRepository _room;

        public RepositoryWrapper(DormyDbContext context)
        {
            _context = context;
        }

        //Create concrete repositories if there aren't
        public IUserRepository User => _user ?? (_user = new UserRepository(_context));
        public IStudentRepository Student => _student ?? (_student = new StudentRepository(_context));
        public IAdminRepository Admin => _admin ?? (_admin = new AdminRepository(_context));
        public INewsRepository News => _news ?? (_news = new NewsRepository(_context));
        public IParamRepository Param => _param ?? (_param = new ParamRepositories.ParamRepository(_context));
        public IParamTypeRepository ParamType => _paramType ?? (_paramType = new ParamTypeRepository(_context));
        public IEquipmentRepository Equipment => _equipment ?? (_equipment = new EquipmentRepository.EquipmentRepository(_context));
        public IRoomRepository Room => _room ?? (_room = new RoomRepository(_context));
    }
}