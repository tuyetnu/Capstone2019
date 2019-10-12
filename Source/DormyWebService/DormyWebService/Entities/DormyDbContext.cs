using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.ContractEntities;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.MoneyEntities;
using DormyWebService.Entities.NewsEntities;
using DormyWebService.Entities.NotificationEntities;
using DormyWebService.Entities.ParamEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Entities.TicketEntities;
using Microsoft.EntityFrameworkCore;

namespace DormyWebService.Entities
{
    public sealed class DormyDbContext:DbContext
    {
        public DormyDbContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        //Register the tables in database
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<PricePerUnit> PricePerUnits { get; set; }
        public DbSet<MoneyTransaction> MoneyTransactions { get; set; }
        public DbSet<RoomMonthlyBill> RoomMonthlyBills { get; set; }
        public DbSet<StudentMonthlyBill> StudentMonthlyBills { get; set; }
        public DbSet<Param> Params { get; set; }
        public DbSet<ParamType> ParamTypes { get; set; }
        public DbSet<CancelContractForm> CancelContractForms { get; set; }
        public DbSet<ContractRenewalForm> ContractRenewalForms { get; set; }
        public DbSet<DormitoryReservationForm> DormitoryReservationForms { get; set; }
        public DbSet<IssueTicket> IssueTickets { get; set; }
        public DbSet<RoomTransferRequestForm> RoomTransferRequestForms { get; set; }
        public DbSet<News> News { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Restrict cascade on delete
            modelBuilder.Entity<Contract>().HasOne(c => c.Student).WithMany(s => s.Contracts)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<StudentMonthlyBill>().HasOne(c => c.Room).WithMany(r => r.StudentMonthlyBills)
                .OnDelete(DeleteBehavior.Restrict);

            //Seed Student Priority Type Table
            modelBuilder.Entity<Role>().HasData(
                new Role {RoleId = 0, Name = "Admin"},
                new Role { RoleId = 1, Name = "Staff" },
                new Role { RoleId = 2, Name = "Student" }
            );

            //Seed ParamType Table
            modelBuilder.Entity<ParamType>().HasData(
                new ParamType() { ParamTypeId = 0, Name = "Contract Status"},
                new ParamType() { ParamTypeId = 1, Name = "EquipmentType" },
                new ParamType() { ParamTypeId = 2, Name = "EquipmentStatus" },
                new ParamType() { ParamTypeId = 3, Name = "MoneyTransactionType" },
                new ParamType() { ParamTypeId = 4, Name = "NotificationType" },
                new ParamType() { ParamTypeId = 5, Name = "NotificationStatus" },
                new ParamType() { ParamTypeId = 6, Name = "RequestStatus" },
                new ParamType() { ParamTypeId = 7, Name = "StudentPriorityType" },
                new ParamType() { ParamTypeId = 8, Name = "NewsStatus" }
            );

        }

    }
}