namespace DormyAppService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RoomTransferRequests", "TargetRoom_Id", "dbo.Rooms");
            DropForeignKey("dbo.RoomTransferRequests", "TargetRoomType_Id", "dbo.RoomTypes");
            DropIndex("dbo.RoomTransferRequests", new[] { "TargetRoom_Id" });
            DropIndex("dbo.RoomTransferRequests", new[] { "TargetRoomType_Id" });
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
            
            AddColumn("dbo.RoomTransferRequests", "Staff_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.RoomTransferRequests", "TargetRoomType_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.RoomTransferRequests", "Staff_Id");
            CreateIndex("dbo.RoomTransferRequests", "TargetRoomType_Id");
            AddForeignKey("dbo.RoomTransferRequests", "Staff_Id", "dbo.Staffs", "Id");
            AddForeignKey("dbo.RoomTransferRequests", "TargetRoomType_Id", "dbo.RoomTypes", "Id", cascadeDelete: true);
            DropColumn("dbo.RoomTransferRequests", "TargetRoom_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoomTransferRequests", "TargetRoom_Id", c => c.Int());
            DropForeignKey("dbo.RoomTransferRequests", "TargetRoomType_Id", "dbo.RoomTypes");
            DropForeignKey("dbo.Contracts", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Contracts", "Status_Id", "dbo.ContractStatus");
            DropForeignKey("dbo.RoomTransferRequests", "Staff_Id", "dbo.Staffs");
            DropIndex("dbo.Contracts", new[] { "Student_Id" });
            DropIndex("dbo.Contracts", new[] { "Status_Id" });
            DropIndex("dbo.RoomTransferRequests", new[] { "TargetRoomType_Id" });
            DropIndex("dbo.RoomTransferRequests", new[] { "Staff_Id" });
            AlterColumn("dbo.RoomTransferRequests", "TargetRoomType_Id", c => c.Int());
            DropColumn("dbo.RoomTransferRequests", "Staff_Id");
            DropTable("dbo.ContractStatus");
            DropTable("dbo.Contracts");
            CreateIndex("dbo.RoomTransferRequests", "TargetRoomType_Id");
            CreateIndex("dbo.RoomTransferRequests", "TargetRoom_Id");
            AddForeignKey("dbo.RoomTransferRequests", "TargetRoomType_Id", "dbo.RoomTypes", "Id");
            AddForeignKey("dbo.RoomTransferRequests", "TargetRoom_Id", "dbo.Rooms", "Id");
        }
    }
}
