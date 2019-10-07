namespace DormyAppService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContractRequests", "Status_Id", "dbo.ContractRequestStatus");
            DropForeignKey("dbo.Contracts", "Status_Id", "dbo.ContractStatus");
            DropForeignKey("dbo.Notifications", "Status_Id", "dbo.NotificationStatus");
            DropForeignKey("dbo.Notifications", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.RoomRequests", "Status_Id", "dbo.RoomRequestStatus");
            DropIndex("dbo.ContractRequests", new[] { "Status_Id" });
            DropIndex("dbo.Contracts", new[] { "Status_Id" });
            DropIndex("dbo.Notifications", new[] { "Status_Id" });
            DropIndex("dbo.Notifications", new[] { "Student_Id" });
            DropIndex("dbo.RoomRequests", new[] { "Status_Id" });
            AddColumn("dbo.ContractRequests", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Contracts", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.IssueTickets", "Point", c => c.Int(nullable: false));
            AddColumn("dbo.Notifications", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Notifications", "Owner_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.RoomRequests", "Month", c => c.Int(nullable: false));
            AddColumn("dbo.RoomRequests", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.Rooms", "Description", c => c.String(maxLength: 100));
            CreateIndex("dbo.Notifications", "Owner_Id");
            AddForeignKey("dbo.Notifications", "Owner_Id", "dbo.ApplicationUsers", "Id");
            DropColumn("dbo.ContractRequests", "Status_Id");
            DropColumn("dbo.Contracts", "Status_Id");
            DropColumn("dbo.EquipmentTypes", "DefaultPrice");
            DropColumn("dbo.MonthlyBills", "WaterPrice");
            DropColumn("dbo.MonthlyBills", "ElectricityPrice");
            DropColumn("dbo.MonthlyBills", "RoomFee");
            DropColumn("dbo.Notifications", "Status_Id");
            DropColumn("dbo.Notifications", "Student_Id");
            DropColumn("dbo.RoomRequests", "Status_Id");
            DropTable("dbo.ContractRequestStatus");
            DropTable("dbo.ContractStatus");
            DropTable("dbo.NotificationStatus");
            DropTable("dbo.RoomRequestStatus");
        }
        
        public override void Down()
        {
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
                "dbo.NotificationStatus",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.ContractRequestStatus",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.RoomRequests", "Status_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Notifications", "Student_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Notifications", "Status_Id", c => c.Int(nullable: false));
            AddColumn("dbo.MonthlyBills", "RoomFee", c => c.Decimal(nullable: false, storeType: "money"));
            AddColumn("dbo.MonthlyBills", "ElectricityPrice", c => c.Decimal(nullable: false, storeType: "money"));
            AddColumn("dbo.MonthlyBills", "WaterPrice", c => c.Decimal(nullable: false, storeType: "money"));
            AddColumn("dbo.EquipmentTypes", "DefaultPrice", c => c.Decimal(nullable: false, storeType: "money"));
            AddColumn("dbo.Contracts", "Status_Id", c => c.Int(nullable: false));
            AddColumn("dbo.ContractRequests", "Status_Id", c => c.Int(nullable: false));
            DropForeignKey("dbo.Notifications", "Owner_Id", "dbo.ApplicationUsers");
            DropIndex("dbo.Notifications", new[] { "Owner_Id" });
            AlterColumn("dbo.Rooms", "Description", c => c.String());
            DropColumn("dbo.RoomRequests", "Status");
            DropColumn("dbo.RoomRequests", "Month");
            DropColumn("dbo.Notifications", "Owner_Id");
            DropColumn("dbo.Notifications", "Status");
            DropColumn("dbo.IssueTickets", "Point");
            DropColumn("dbo.Contracts", "Status");
            DropColumn("dbo.ContractRequests", "Status");
            CreateIndex("dbo.RoomRequests", "Status_Id");
            CreateIndex("dbo.Notifications", "Student_Id");
            CreateIndex("dbo.Notifications", "Status_Id");
            CreateIndex("dbo.Contracts", "Status_Id");
            CreateIndex("dbo.ContractRequests", "Status_Id");
            AddForeignKey("dbo.RoomRequests", "Status_Id", "dbo.RoomRequestStatus", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Notifications", "Student_Id", "dbo.Students", "Id");
            AddForeignKey("dbo.Notifications", "Status_Id", "dbo.NotificationStatus", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Contracts", "Status_Id", "dbo.ContractStatus", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ContractRequests", "Status_Id", "dbo.ContractRequestStatus", "Id", cascadeDelete: true);
        }
    }
}
