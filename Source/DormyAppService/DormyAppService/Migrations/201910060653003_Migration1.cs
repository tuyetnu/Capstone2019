namespace DormyAppService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.RoomTransferRequests",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                        Student_Id = c.String(nullable: false, maxLength: 128),
                        TargetRoom_Id = c.Int(),
                        TargetRoomType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoomTransferStatus", t => t.Id)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .ForeignKey("dbo.Rooms", t => t.TargetRoom_Id)
                .ForeignKey("dbo.RoomTypes", t => t.TargetRoomType_Id)
                .Index(t => t.Id)
                .Index(t => t.Student_Id)
                .Index(t => t.TargetRoom_Id)
                .Index(t => t.TargetRoomType_Id);
            
            CreateTable(
                "dbo.RoomTransferStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.EvaluationScoreHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        PreviousScore = c.Int(nullable: false),
                        ResultedScore = c.Int(nullable: false),
                        CreatedBy_Id = c.String(maxLength: 128),
                        TargetStudent_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Staffs", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Students", t => t.TargetStudent_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.TargetStudent_Id);
            
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
                        EquipmentType_Id = c.Int(nullable: false),
                        Room_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EquipmentTypes", t => t.EquipmentType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.Room_Id, cascadeDelete: true)
                .Index(t => t.EquipmentType_Id)
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.EquipmentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoomTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        DefaultCapacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoomTransferHistories",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        RoomTransferRequest_Id = c.Int(nullable: false),
                        Staff_Id = c.String(maxLength: 128),
                        Student_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoomTransferRequests", t => t.RoomTransferRequest_Id, cascadeDelete: true)
                .ForeignKey("dbo.Staffs", t => t.Staff_Id)
                .ForeignKey("dbo.RoomTransferStatus", t => t.Id)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .Index(t => t.Id)
                .Index(t => t.RoomTransferRequest_Id)
                .Index(t => t.Staff_Id)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PriorityType_Id = c.Int(),
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
                .ForeignKey("dbo.StudentPriorityTypes", t => t.PriorityType_Id)
                .ForeignKey("dbo.Rooms", t => t.Room_Id)
                .Index(t => t.Id)
                .Index(t => t.PriorityType_Id)
                .Index(t => t.Room_Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staffs", "Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Students", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.Students", "PriorityType_Id", "dbo.StudentPriorityTypes");
            DropForeignKey("dbo.Students", "Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.RoomTransferRequests", "TargetRoomType_Id", "dbo.RoomTypes");
            DropForeignKey("dbo.RoomTransferRequests", "TargetRoom_Id", "dbo.Rooms");
            DropForeignKey("dbo.RoomTransferRequests", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.RoomTransferHistories", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.RoomTransferHistories", "Id", "dbo.RoomTransferStatus");
            DropForeignKey("dbo.RoomTransferHistories", "Staff_Id", "dbo.Staffs");
            DropForeignKey("dbo.RoomTransferHistories", "RoomTransferRequest_Id", "dbo.RoomTransferRequests");
            DropForeignKey("dbo.Rooms", "RoomType_Id", "dbo.RoomTypes");
            DropForeignKey("dbo.Equipments", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.Equipments", "EquipmentType_Id", "dbo.EquipmentTypes");
            DropForeignKey("dbo.EvaluationScoreHistories", "TargetStudent_Id", "dbo.Students");
            DropForeignKey("dbo.EvaluationScoreHistories", "CreatedBy_Id", "dbo.Staffs");
            DropForeignKey("dbo.RoomTransferRequests", "Id", "dbo.RoomTransferStatus");
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropIndex("dbo.Staffs", new[] { "Id" });
            DropIndex("dbo.Students", new[] { "Room_Id" });
            DropIndex("dbo.Students", new[] { "PriorityType_Id" });
            DropIndex("dbo.Students", new[] { "Id" });
            DropIndex("dbo.RoomTransferHistories", new[] { "Student_Id" });
            DropIndex("dbo.RoomTransferHistories", new[] { "Staff_Id" });
            DropIndex("dbo.RoomTransferHistories", new[] { "RoomTransferRequest_Id" });
            DropIndex("dbo.RoomTransferHistories", new[] { "Id" });
            DropIndex("dbo.Equipments", new[] { "Room_Id" });
            DropIndex("dbo.Equipments", new[] { "EquipmentType_Id" });
            DropIndex("dbo.Rooms", new[] { "RoomType_Id" });
            DropIndex("dbo.IdentityUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.EvaluationScoreHistories", new[] { "TargetStudent_Id" });
            DropIndex("dbo.EvaluationScoreHistories", new[] { "CreatedBy_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.RoomTransferRequests", new[] { "TargetRoomType_Id" });
            DropIndex("dbo.RoomTransferRequests", new[] { "TargetRoom_Id" });
            DropIndex("dbo.RoomTransferRequests", new[] { "Student_Id" });
            DropIndex("dbo.RoomTransferRequests", new[] { "Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropTable("dbo.Staffs");
            DropTable("dbo.Students");
            DropTable("dbo.RoomTransferHistories");
            DropTable("dbo.RoomTypes");
            DropTable("dbo.EquipmentTypes");
            DropTable("dbo.Equipments");
            DropTable("dbo.Rooms");
            DropTable("dbo.StudentPriorityTypes");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.EvaluationScoreHistories");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.RoomTransferStatus");
            DropTable("dbo.RoomTransferRequests");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityRoles");
        }
    }
}
