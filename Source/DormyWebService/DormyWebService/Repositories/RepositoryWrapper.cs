using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities;
using DormyWebService.Repositories.ContractRepositories;
using DormyWebService.Repositories.EquipmentRepositories;
using DormyWebService.Repositories.MoneyRepositories;
using DormyWebService.Repositories.NewsRepositories;
using DormyWebService.Repositories.NotificationRepositories;
using DormyWebService.Repositories.ParamRepositories;
using DormyWebService.Repositories.RoomRepositories;
using DormyWebService.Repositories.StaffRepositories;
using DormyWebService.Repositories.TicketRepositories;
using DormyWebService.Repositories.UserRepositories;
using Microsoft.EntityFrameworkCore;

namespace DormyWebService.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        public readonly DormyDbContext Context;
        private IUserRepository _user;
        private IStudentRepository _student;
        private IAdminRepository _admin;
        private IStaffRepository _staff;
        private INewsRepository _news;
        private IParamRepository _param;
        private IParamTypeRepository _paramType;
        private IEquipmentRepository _equipment;
        private IRoomRepository _room;
        private IBuildingRepository _building;
        private IRoomGroupRepository _roomGroup;
        private IRoomGroupsAndStaffRepository _roomGroupsAndStaff;
        private IRoomTypesAndEquipmentTypesRepository _roomTypesAndEquipmentTypes;
        private IRoomsAndEquipmentTypesRepository _roomsAndEquipmentTypes;
        private IContractRepository _contract;
        private IRenewContractRepository _renewContract;
        private ICancelContractRepository _cancelContract;
        private IRoomBookingRepository _roomBooking;
        private IRoomTransferRepository _roomTransfer;
        private IIssueTicketRepository _issueTicket;
        private IMoneyTransactionRepository _moneyTransaction;
        private IRoomMonthlyBillRepository _roomMonthlyBill;
        private IStudentMonthlyBillRepository _studentMonthlyBill;
        private INotificationRepository _notification;
        private IPricePerUnitRepository _pricePerUnit;
        
        public RepositoryWrapper(DormyDbContext context)
        {
            Context = context;
        }

        //For saving multiple changes
        public async Task Save()
        { 
            await Context.SaveChangesAsync();
        }

        public void DeleteChanges()
        {
            var changedEntriesCopy = Context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }

        //Create concrete repositories if there aren't
        public IUserRepository User => _user ?? (_user = new UserRepository(Context));
        public IStudentRepository Student => _student ?? (_student = new StudentRepository(Context));
        public IAdminRepository Admin => _admin ?? (_admin = new AdminRepository(Context));
        public IStaffRepository Staff => _staff ?? (_staff = new StaffRepository(Context));
        public INewsRepository News => _news ?? (_news = new NewsRepository(Context));
        public IParamRepository Param => _param ?? (_param = new ParamRepositories.ParamRepository(Context));
        public IParamTypeRepository ParamType => _paramType ?? (_paramType = new ParamTypeRepository(Context));
        public IEquipmentRepository Equipment => _equipment ?? (_equipment = new EquipmentRepositories.EquipmentRepository(Context));
        public IRoomRepository Room => _room ?? (_room = new RoomRepository(Context));
        public IBuildingRepository Building => _building ?? (_building = new BuildingRepository(Context));
        public IRoomGroupRepository RoomGroup => _roomGroup ?? (_roomGroup = new RoomGroupRepository(Context));
        public IRoomsAndEquipmentTypesRepository RoomsAndEquipmentTypes => _roomsAndEquipmentTypes ?? (_roomsAndEquipmentTypes = new RoomsAndEquipmentTypesRepository(Context));
        public IRoomGroupsAndStaffRepository RoomGroupsAndStaff => _roomGroupsAndStaff ?? (_roomGroupsAndStaff = new RoomGroupsAndStaffRepository(Context));
        public IRoomTypesAndEquipmentTypesRepository RoomTypesAndEquipmentTypes => _roomTypesAndEquipmentTypes ?? (_roomTypesAndEquipmentTypes = new RoomTypesAndEquipmentTypesRepository(Context));
        public IContractRepository Contract => _contract ?? (_contract = new ContractRepository(Context));
        public IRenewContractRepository RenewContract => _renewContract ?? (_renewContract = new RenewContractRepository(Context));
        public ICancelContractRepository CancelContract => _cancelContract ?? (_cancelContract = new CancelContractRepository(Context));
        public IRoomBookingRepository RoomBooking => _roomBooking ?? (_roomBooking = new RoomBookingRepository(Context));
        public IRoomTransferRepository RoomTransfer => _roomTransfer ?? (_roomTransfer = new RoomTransferRepository(Context));
        public IIssueTicketRepository IssueTicket => _issueTicket ?? (_issueTicket = new IssueTicketRepository(Context));
        public IMoneyTransactionRepository MoneyTransaction => _moneyTransaction ?? (_moneyTransaction = new MoneyTransactionRepository(Context));
        public IRoomMonthlyBillRepository RoomMonthlyBill => _roomMonthlyBill ?? (_roomMonthlyBill = new RoomMonthlyBillRepository(Context));
        public IStudentMonthlyBillRepository StudentMonthlyBill => _studentMonthlyBill ?? (_studentMonthlyBill = new StudentMonthlyBillRepository(Context));
        public INotificationRepository Notification => _notification ?? (_notification = new NotificationRepository(Context));
        public IPricePerUnitRepository PricePerUnit => _pricePerUnit ?? (_pricePerUnit = new PricePerUnitRepository(Context));
    }
}