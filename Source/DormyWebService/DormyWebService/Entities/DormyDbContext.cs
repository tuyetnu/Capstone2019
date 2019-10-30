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
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<RoomTypes_EquipmentTypes> RoomTypesEquipmentTypes { get; set; }
        public DbSet<PricePerUnit> PricePerUnits { get; set; }
        public DbSet<MoneyTransaction> MoneyTransactions { get; set; }
        public DbSet<RoomMonthlyBill> RoomMonthlyBills { get; set; }
        public DbSet<StudentMonthlyBill> StudentMonthlyBills { get; set; }
        public DbSet<Param> Params { get; set; }
        public DbSet<ParamType> ParamTypes { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<CancelContractForm> CancelContractForms { get; set; }
        public DbSet<ContractRenewalForm> ContractRenewalForms { get; set; }
        public DbSet<RoomBookingRequestForm> RoomBookingRequestForm { get; set; }
        public DbSet<IssueTicket> IssueTickets { get; set; }
        public DbSet<RoomTransferRequestForm> RoomTransferRequestForms { get; set; }
        public DbSet<News> News { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Restrict cascade on delete
            modelBuilder.Entity<StudentMonthlyBill>().HasOne(c => c.Room).WithMany(r => r.StudentMonthlyBills)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ContractRenewalForm>().HasOne(c => c.Contract).WithMany(r => r.ContractRenewalForms)
                .OnDelete(DeleteBehavior.Restrict);

            //Configure
            modelBuilder.Entity<RoomTypes_EquipmentTypes>().HasKey(x => new {
                x.EquipmentTypeId,
                x.RoomTypeId
            });

            //Seed ParamType Table
//            modelBuilder.Entity<ParamType>().HasData(
//                new ParamType() { ParamTypeId = 0, Name = "MoneyTransactionType" },
//                new ParamType() { ParamTypeId = 1, Name = "NotificationType" },
//                new ParamType() { ParamTypeId = 2, Name = "StudentPriorityType" },
//                new ParamType() { ParamTypeId = 3, Name = "AcceptedEmailHost" },
//                new ParamType() { ParamTypeId = 4, Name = "RoomType" },
//                new ParamType() { ParamTypeId = 5, Name = "DepositMoneyParam" },
//                new ParamType() { ParamTypeId = 6, Name = "IssueType" },
//                new ParamType() { ParamTypeId = 7, Name = "EquipmentType" },
//                new ParamType() { ParamTypeId = 8, Name = "EvaluationParam" }
//                );

            //Seed Param Table
//            modelBuilder.Entity<Param>().HasData(
//                new Param() { ParamId = 0, Name = "Ưu tiên loại 1", ParamTypeId = 2},
//                new Param() { ParamId = 1, Name = "Ưu tiên loại 2", ParamTypeId = 2 },
//                new Param() { ParamId = 2, Name = "Ưu tiên loại 1 và 2", ParamTypeId = 2 },
//                new Param() { ParamId = 3, Name = "Không có ưu tiên", ParamTypeId = 2 },
//                new Param() { ParamId = 4, Name = "Bed", ParamTypeId = 7 },
//                new Param() { ParamId = 5, Name = "Table", ParamTypeId = 7 },
//                new Param() { ParamId = 6, Name = "Wardrobe", ParamTypeId = 7 },
//                new Param() { ParamId = 7, Name = "Fan", ParamTypeId = 7 },
//                new Param() { ParamId = 8, Name = "Wifi", ParamTypeId = 7 },
//                new Param() { ParamId = 9, Name = "Air Conditioner", ParamTypeId = 7 },
//                new Param() { ParamId = 10, Name = "Fridge",ParamTypeId = 7},
//                new Param() { ParamId = 11, Name = "Standard Room", ParamTypeId = 4 },
//                new Param() { ParamId = 12, Name = "Service Room", ParamTypeId = 4 },
//                new Param() { ParamId = 13, Name = "DepositMoney", ParamTypeId = 5 },
//                new Param() { ParamId = 14, Name = "DepositMoneyUpperLimit", ParamTypeId = 5 , Value = 10000},
//                new Param() { ParamId = 15, Name = "DepositMoneyLowerLimit", ParamTypeId = 5 , Value = 10000000},
//                new Param() { ParamId = 16, Name = "DepositMoneyStep", ParamTypeId = 5, Value = 500 },
//                new Param() { ParamId = 17, Name = "Tình trạng thiết bị", ParamTypeId = 6},
//                new Param() { ParamId = 18, Name = "Phản ánh", ParamTypeId = 6},
//                new Param() { ParamId = 19, Name = "Default Evaluation Point", ParamTypeId = 8 , Value = 100}
//            );

            //Seed RoomTypes_EquipmentTypes table


        }

    }
}