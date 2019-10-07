namespace DormyAppService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContractRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Month = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Staff_Id = c.String(maxLength: 128),
                        Status_Id = c.Int(nullable: false),
                        Student_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Staffs", t => t.Staff_Id)
                .ForeignKey("dbo.ContractRequestStatus", t => t.Status_Id, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .Index(t => t.Staff_Id)
                .Index(t => t.Status_Id)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.ContractRequestStatus",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Status_Id = c.Int(nullable: false),
                        Student_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContractStatus", t => t.Status_Id, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .Index(t => t.Status_Id)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.ContractStatus",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentPriorityTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                        Capacity = c.Int(nullable: false),
                        RoomType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoomTypes", t => t.RoomType_Id, cascadeDelete: true)
                .Index(t => t.RoomType_Id);
            
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        ImageUrl = c.String(),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        EquipmentType_Id = c.Int(nullable: false),
                        Room_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EquipmentTypes", t => t.EquipmentType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.Room_Id)
                .Index(t => t.EquipmentType_Id)
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.EquipmentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(maxLength: 100),
                        DefaultPrice = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoomTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(maxLength: 100),
                        DefaultCapacity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ImageUrl = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                        Student_Id = c.String(nullable: false, maxLength: 128),
                        Type_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .ForeignKey("dbo.DocumentTypes", t => t.Type_Id, cascadeDelete: true)
                .Index(t => t.Student_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.DocumentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IssueTickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                        ImageUrl = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                        Equipment_Id = c.Int(),
                        Owner_Id = c.String(nullable: false, maxLength: 128),
                        Room_Id = c.Int(),
                        Staff_Id = c.String(maxLength: 128),
                        Status_Id = c.Int(nullable: false),
                        TargetUser_Id = c.String(maxLength: 128),
                        Type_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equipments", t => t.Equipment_Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.Owner_Id, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.Room_Id)
                .ForeignKey("dbo.Staffs", t => t.Staff_Id)
                .ForeignKey("dbo.IssueStatus", t => t.Status_Id, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUsers", t => t.TargetUser_Id)
                .ForeignKey("dbo.IssueTypes", t => t.Type_Id, cascadeDelete: true)
                .Index(t => t.Equipment_Id)
                .Index(t => t.Owner_Id)
                .Index(t => t.Room_Id)
                .Index(t => t.Staff_Id)
                .Index(t => t.Status_Id)
                .Index(t => t.TargetUser_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.IssueStatus",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IssueTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MoneyTransactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OriginalBalance = c.Decimal(nullable: false, storeType: "money"),
                        MoneyAmount = c.Decimal(nullable: false, storeType: "money"),
                        ResultBalance = c.Decimal(nullable: false, storeType: "money"),
                        Date = c.DateTime(nullable: false),
                        Room_Id = c.Int(nullable: false),
                        Student_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.Room_Id, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .Index(t => t.Room_Id)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.MonthlyBills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsPaid = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                        PreviousWaterNumber = c.Int(nullable: false),
                        NewWaterNumber = c.Int(nullable: false),
                        WaterPrice = c.Decimal(nullable: false, storeType: "money"),
                        WaterBill = c.Decimal(nullable: false, storeType: "money"),
                        PreviousElectricityNumber = c.Int(nullable: false),
                        NewElectricityNumber = c.Int(nullable: false),
                        ElectricityPrice = c.Decimal(nullable: false, storeType: "money"),
                        ElectricityBill = c.Decimal(nullable: false, storeType: "money"),
                        RoomFee = c.Decimal(nullable: false, storeType: "money"),
                        TotalAmount = c.Decimal(nullable: false, storeType: "money"),
                        Room_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.Room_Id, cascadeDelete: true)
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(maxLength: 200),
                        Url = c.String(),
                        Date = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                        Status_Id = c.Int(nullable: false),
                        Student_Id = c.String(maxLength: 128),
                        Type_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NotificationStatus", t => t.Status_Id, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .ForeignKey("dbo.NotificationTypes", t => t.Type_Id, cascadeDelete: true)
                .Index(t => t.Status_Id)
                .Index(t => t.Student_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.NotificationStatus",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NotificationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoomRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                        Staff_Id = c.String(maxLength: 128),
                        Status_Id = c.Int(nullable: false),
                        Student_Id = c.String(nullable: false, maxLength: 128),
                        TargetRoomType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Staffs", t => t.Staff_Id)
                .ForeignKey("dbo.RoomRequestStatus", t => t.Status_Id, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .ForeignKey("dbo.RoomTypes", t => t.TargetRoomType_Id, cascadeDelete: true)
                .Index(t => t.Staff_Id)
                .Index(t => t.Status_Id)
                .Index(t => t.Student_Id)
                .Index(t => t.TargetRoomType_Id);
            
            CreateTable(
                "dbo.RoomRequestStatus",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IdentityCardId = c.String(),
                        HomeTown = c.String(),
                        Gender = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PriorityType_Id = c.Int(nullable: false),
                        Room_Id = c.Int(),
                        StudentCardPictureUrl = c.String(),
                        StartedSchoolYear = c.Int(nullable: false),
                        Term = c.Int(nullable: false),
                        IsRoomLeader = c.Boolean(nullable: false),
                        EvaluationScore = c.Int(nullable: false),
                        AccountBalance = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.Id)
                .ForeignKey("dbo.StudentPriorityTypes", t => t.PriorityType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.Room_Id)
                .Index(t => t.Id)
                .Index(t => t.PriorityType_Id)
                .Index(t => t.Room_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.Students", "PriorityType_Id", "dbo.StudentPriorityTypes");
            DropForeignKey("dbo.Students", "Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Staffs", "Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.RoomRequests", "TargetRoomType_Id", "dbo.RoomTypes");
            DropForeignKey("dbo.RoomRequests", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.RoomRequests", "Status_Id", "dbo.RoomRequestStatus");
            DropForeignKey("dbo.RoomRequests", "Staff_Id", "dbo.Staffs");
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.Notifications", "Type_Id", "dbo.NotificationTypes");
            DropForeignKey("dbo.Notifications", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Notifications", "Status_Id", "dbo.NotificationStatus");
            DropForeignKey("dbo.MonthlyBills", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.MoneyTransactions", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.MoneyTransactions", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.IssueTickets", "Type_Id", "dbo.IssueTypes");
            DropForeignKey("dbo.IssueTickets", "TargetUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IssueTickets", "Status_Id", "dbo.IssueStatus");
            DropForeignKey("dbo.IssueTickets", "Staff_Id", "dbo.Staffs");
            DropForeignKey("dbo.IssueTickets", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.IssueTickets", "Owner_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IssueTickets", "Equipment_Id", "dbo.Equipments");
            DropForeignKey("dbo.Documents", "Type_Id", "dbo.DocumentTypes");
            DropForeignKey("dbo.Documents", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.ContractRequests", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Rooms", "RoomType_Id", "dbo.RoomTypes");
            DropForeignKey("dbo.Equipments", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.Equipments", "EquipmentType_Id", "dbo.EquipmentTypes");
            DropForeignKey("dbo.Contracts", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Contracts", "Status_Id", "dbo.ContractStatus");
            DropForeignKey("dbo.ContractRequests", "Status_Id", "dbo.ContractRequestStatus");
            DropForeignKey("dbo.ContractRequests", "Staff_Id", "dbo.Staffs");
            DropIndex("dbo.Students", new[] { "Room_Id" });
            DropIndex("dbo.Students", new[] { "PriorityType_Id" });
            DropIndex("dbo.Students", new[] { "Id" });
            DropIndex("dbo.Staffs", new[] { "Id" });
            DropIndex("dbo.RoomRequests", new[] { "TargetRoomType_Id" });
            DropIndex("dbo.RoomRequests", new[] { "Student_Id" });
            DropIndex("dbo.RoomRequests", new[] { "Status_Id" });
            DropIndex("dbo.RoomRequests", new[] { "Staff_Id" });
            DropIndex("dbo.Notifications", new[] { "Type_Id" });
            DropIndex("dbo.Notifications", new[] { "Student_Id" });
            DropIndex("dbo.Notifications", new[] { "Status_Id" });
            DropIndex("dbo.MonthlyBills", new[] { "Room_Id" });
            DropIndex("dbo.MoneyTransactions", new[] { "Student_Id" });
            DropIndex("dbo.MoneyTransactions", new[] { "Room_Id" });
            DropIndex("dbo.IssueTickets", new[] { "Type_Id" });
            DropIndex("dbo.IssueTickets", new[] { "TargetUser_Id" });
            DropIndex("dbo.IssueTickets", new[] { "Status_Id" });
            DropIndex("dbo.IssueTickets", new[] { "Staff_Id" });
            DropIndex("dbo.IssueTickets", new[] { "Room_Id" });
            DropIndex("dbo.IssueTickets", new[] { "Owner_Id" });
            DropIndex("dbo.IssueTickets", new[] { "Equipment_Id" });
            DropIndex("dbo.Documents", new[] { "Type_Id" });
            DropIndex("dbo.Documents", new[] { "Student_Id" });
            DropIndex("dbo.Equipments", new[] { "Room_Id" });
            DropIndex("dbo.Equipments", new[] { "EquipmentType_Id" });
            DropIndex("dbo.Rooms", new[] { "RoomType_Id" });
            DropIndex("dbo.Contracts", new[] { "Student_Id" });
            DropIndex("dbo.Contracts", new[] { "Status_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ContractRequests", new[] { "Student_Id" });
            DropIndex("dbo.ContractRequests", new[] { "Status_Id" });
            DropIndex("dbo.ContractRequests", new[] { "Staff_Id" });
            DropTable("dbo.Students");
            DropTable("dbo.Staffs");
            DropTable("dbo.RoomRequestStatus");
            DropTable("dbo.RoomRequests");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.NotificationTypes");
            DropTable("dbo.NotificationStatus");
            DropTable("dbo.Notifications");
            DropTable("dbo.MonthlyBills");
            DropTable("dbo.MoneyTransactions");
            DropTable("dbo.IssueTypes");
            DropTable("dbo.IssueStatus");
            DropTable("dbo.IssueTickets");
            DropTable("dbo.DocumentTypes");
            DropTable("dbo.Documents");
            DropTable("dbo.RoomTypes");
            DropTable("dbo.EquipmentTypes");
            DropTable("dbo.Equipments");
            DropTable("dbo.Rooms");
            DropTable("dbo.StudentPriorityTypes");
            DropTable("dbo.ContractStatus");
            DropTable("dbo.Contracts");
            DropTable("dbo.ContractRequestStatus");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.ContractRequests");
        }
    }
}
