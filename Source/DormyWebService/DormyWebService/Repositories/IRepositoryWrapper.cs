using System.Threading.Tasks;
using DormyWebService.Entities.NewsEntities;
using DormyWebService.Repositories.ContractRepositories;
using DormyWebService.Repositories.EquipmentRepositories;
using DormyWebService.Repositories.MoneyRepositories;
using DormyWebService.Repositories.NewsRepositories;
using DormyWebService.Repositories.ParamRepositories;
using DormyWebService.Repositories.RoomRepositories;
using DormyWebService.Repositories.StaffRepositories;
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

        IStaffRepository Staff { get; }

        INewsRepository News { get; }

        IParamRepository Param { get; }

        IParamTypeRepository ParamType { get;}

        IEquipmentRepository Equipment { get; }

        IRoomRepository Room { get; }
        IBuildingRepository Building { get; }
        IContractRepository Contract { get; }
        IRenewContractRepository RenewContract { get;}
        ICancelContractRepository CancelContract { get;}

        IRoomBookingRepository RoomBooking { get; }

        IRoomTransferRepository RoomTransfer { get; }

        IIssueTicketRepository IssueTicket { get; }

        IMoneyTransactionRepository MoneyTransaction { get; }

        IRoomMonthlyBillRepository RoomMonthlyBill { get; }

        IStudentMonthlyBillRepository StudentMonthlyBill { get; }
    }
}