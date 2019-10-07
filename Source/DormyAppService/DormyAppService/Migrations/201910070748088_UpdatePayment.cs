namespace DormyAppService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePayment : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MonthlyBills", newName: "RoomMonthlyBills");
            DropForeignKey("dbo.Documents", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Documents", "Type_Id", "dbo.DocumentTypes");
            DropForeignKey("dbo.IssueTickets", "Status_Id", "dbo.IssueStatus");
            DropIndex("dbo.Documents", new[] { "Student_Id" });
            DropIndex("dbo.Documents", new[] { "Type_Id" });
            DropIndex("dbo.IssueTickets", new[] { "Status_Id" });
            CreateTable(
                "dbo.StudentMonthlyBills",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                        RoomUtilityFee = c.Decimal(nullable: false, storeType: "money"),
                        Percentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UtilityFee = c.Decimal(nullable: false, storeType: "money"),
                        RoomFee = c.Decimal(nullable: false, storeType: "money"),
                        Total = c.Decimal(nullable: false, storeType: "money"),
                        RoomType_Id = c.Int(nullable: false),
                        Student_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoomMonthlyBills", t => t.Id)
                .ForeignKey("dbo.RoomTypes", t => t.RoomType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .Index(t => t.Id)
                .Index(t => t.RoomType_Id)
                .Index(t => t.Student_Id);
            
            AddColumn("dbo.Students", "IdentityNumber", c => c.String(nullable: false));
            AddColumn("dbo.Students", "IdentityCardImageUrl", c => c.String());
            AddColumn("dbo.Students", "StudentCardNumber", c => c.String(nullable: false));
            AddColumn("dbo.Students", "StudentCardImageUrl", c => c.String());
            AddColumn("dbo.Students", "PriorityImageUrl", c => c.String());
            AddColumn("dbo.IssueTickets", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.RoomMonthlyBills", "TargetMonth", c => c.Int(nullable: false));
            AddColumn("dbo.RoomMonthlyBills", "TargetYear", c => c.Int(nullable: false));
            AddColumn("dbo.RoomMonthlyBills", "TotalRoomFee", c => c.Decimal(nullable: false, storeType: "money"));
            DropColumn("dbo.Students", "StudentCardPictureUrl");
            DropColumn("dbo.IssueTickets", "Status_Id");
            DropTable("dbo.Documents");
            DropTable("dbo.DocumentTypes");
            DropTable("dbo.IssueStatus");
        }
        
        public override void Down()
        {
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
                "dbo.DocumentTypes",
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
                        Name = c.String(nullable: false),
                        ImageUrl = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                        Student_Id = c.String(nullable: false, maxLength: 128),
                        Type_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.IssueTickets", "Status_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "StudentCardPictureUrl", c => c.String());
            DropForeignKey("dbo.StudentMonthlyBills", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.StudentMonthlyBills", "RoomType_Id", "dbo.RoomTypes");
            DropForeignKey("dbo.StudentMonthlyBills", "Id", "dbo.RoomMonthlyBills");
            DropIndex("dbo.StudentMonthlyBills", new[] { "Student_Id" });
            DropIndex("dbo.StudentMonthlyBills", new[] { "RoomType_Id" });
            DropIndex("dbo.StudentMonthlyBills", new[] { "Id" });
            DropColumn("dbo.RoomMonthlyBills", "TotalRoomFee");
            DropColumn("dbo.RoomMonthlyBills", "TargetYear");
            DropColumn("dbo.RoomMonthlyBills", "TargetMonth");
            DropColumn("dbo.IssueTickets", "Status");
            DropColumn("dbo.Students", "PriorityImageUrl");
            DropColumn("dbo.Students", "StudentCardImageUrl");
            DropColumn("dbo.Students", "StudentCardNumber");
            DropColumn("dbo.Students", "IdentityCardImageUrl");
            DropColumn("dbo.Students", "IdentityNumber");
            DropTable("dbo.StudentMonthlyBills");
            CreateIndex("dbo.IssueTickets", "Status_Id");
            CreateIndex("dbo.Documents", "Type_Id");
            CreateIndex("dbo.Documents", "Student_Id");
            AddForeignKey("dbo.IssueTickets", "Status_Id", "dbo.IssueStatus", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Documents", "Type_Id", "dbo.DocumentTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Documents", "Student_Id", "dbo.Students", "Id");
            RenameTable(name: "dbo.RoomMonthlyBills", newName: "MonthlyBills");
        }
    }
}
