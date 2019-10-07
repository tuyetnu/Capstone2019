namespace DormyAppService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseV2 : DbMigration
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
                "dbo.ContractRequestStatus",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
                        Type_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NotificationStatus", t => t.Status_Id, cascadeDelete: true)
                .ForeignKey("dbo.NotificationTypes", t => t.Type_Id, cascadeDelete: true)
                .Index(t => t.Status_Id)
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "Type_Id", "dbo.NotificationTypes");
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
            DropForeignKey("dbo.IssueTickets", "Equipment_Id", "dbo.Equipments");
            DropForeignKey("dbo.Documents", "Type_Id", "dbo.DocumentTypes");
            DropForeignKey("dbo.Documents", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.ContractRequests", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.ContractRequests", "Status_Id", "dbo.ContractRequestStatus");
            DropForeignKey("dbo.ContractRequests", "Staff_Id", "dbo.Staffs");
            DropIndex("dbo.Notifications", new[] { "Type_Id" });
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
            DropIndex("dbo.ContractRequests", new[] { "Student_Id" });
            DropIndex("dbo.ContractRequests", new[] { "Status_Id" });
            DropIndex("dbo.ContractRequests", new[] { "Staff_Id" });
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
            DropTable("dbo.ContractRequestStatus");
            DropTable("dbo.ContractRequests");
        }
    }
}
