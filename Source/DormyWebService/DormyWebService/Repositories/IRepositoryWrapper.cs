using System.Threading.Tasks;
using DormyWebService.Entities.NewsEntities;
using DormyWebService.Repositories.EquipmentRepositories;
using DormyWebService.Repositories.NewsRepositories;
using DormyWebService.Repositories.ParamRepositories;
using DormyWebService.Repositories.RoomRepositories;
using DormyWebService.Repositories.TicketRepositories;
using DormyWebService.Repositories.UserRepositories;

namespace DormyWebService.Repositories
{
    public interface IRepositoryWrapper
    {
        //For saving multiple changes
        Task Save();

        void DeleteChanges();

        IUserRepository User { get; }

        IStudentRepository Student { get; }

        IAdminRepository Admin { get; }

        INewsRepository News { get; }

        IParamRepository Param { get; }

        IParamTypeRepository ParamType { get;}

        IEquipmentRepository Equipment { get; }

        IRoomRepository Room { get; }

        IRoomBookingRepository RoomBooking { get; }

        IRoomTransferRepository RoomTransfer { get; }

        IIssueTicketRepository IssueTicket { get; }
    }
}