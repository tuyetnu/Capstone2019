using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities;
using DormyWebService.Repositories.EquipmentRepositories;
using DormyWebService.Repositories.NewsRepositories;
using DormyWebService.Repositories.ParamRepositories;
using DormyWebService.Repositories.RoomRepositories;
using DormyWebService.Repositories.TicketRepositories;
using DormyWebService.Repositories.UserRepositories;
using Microsoft.EntityFrameworkCore;

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
        private IRoomBookingRepository _roomBooking;
        private IRoomTransferRepository _roomTransfer;
        private IIssueTicketRepository _issueTicket;

        public RepositoryWrapper(DormyDbContext context)
        {
            _context = context;
        }

        //For saving multiple changes
        public async Task Save()
        { 
            await _context.SaveChangesAsync();
        }

        public void DeleteChanges()
        {
            var changedEntriesCopy = _context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }

        //Create concrete repositories if there aren't
        public IUserRepository User => _user ?? (_user = new UserRepository(_context));
        public IStudentRepository Student => _student ?? (_student = new StudentRepository(_context));
        public IAdminRepository Admin => _admin ?? (_admin = new AdminRepository(_context));
        public INewsRepository News => _news ?? (_news = new NewsRepository(_context));
        public IParamRepository Param => _param ?? (_param = new ParamRepositories.ParamRepository(_context));
        public IParamTypeRepository ParamType => _paramType ?? (_paramType = new ParamTypeRepository(_context));
        public IEquipmentRepository Equipment => _equipment ?? (_equipment = new EquipmentRepositories.EquipmentRepository(_context));
        public IRoomRepository Room => _room ?? (_room = new RoomRepository(_context));
        public IRoomBookingRepository RoomBooking => _roomBooking ?? (_roomBooking = new RoomBookingRepository(_context));
        public IRoomTransferRepository RoomTransfer => _roomTransfer ?? (_roomTransfer = new RoomTransferRepository(_context));
        public IIssueTicketRepository IssueTicket => _issueTicket ?? (_issueTicket = new IssueTicketRepository(_context));
    }
}