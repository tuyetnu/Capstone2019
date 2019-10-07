namespace DormyAppService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseV1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.RoomTransferRequests", newName: "RoomRequests");
            RenameTable(name: "dbo.RoomTransferStatus", newName: "RoomRequestStatus");
            DropForeignKey("dbo.Students", "PriorityType_Id", "dbo.StudentPriorityTypes");
            DropIndex("dbo.Students", new[] { "PriorityType_Id" });
            AddColumn("dbo.Equipments", "ImageUrl", c => c.String());
            AddColumn("dbo.Equipments", "ExpirationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.EquipmentTypes", "Description", c => c.String(maxLength: 100));
            AlterColumn("dbo.Students", "PriorityType_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.EquipmentTypes", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.RoomTypes", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.RoomTypes", "Description", c => c.String(maxLength: 100));
            CreateIndex("dbo.Students", "PriorityType_Id");
            AddForeignKey("dbo.Students", "PriorityType_Id", "dbo.StudentPriorityTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "PriorityType_Id", "dbo.StudentPriorityTypes");
            DropIndex("dbo.Students", new[] { "PriorityType_Id" });
            AlterColumn("dbo.RoomTypes", "Description", c => c.String());
            AlterColumn("dbo.RoomTypes", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.EquipmentTypes", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "PriorityType_Id", c => c.Int());
            DropColumn("dbo.EquipmentTypes", "Description");
            DropColumn("dbo.Equipments", "ExpirationDate");
            DropColumn("dbo.Equipments", "ImageUrl");
            CreateIndex("dbo.Students", "PriorityType_Id");
            AddForeignKey("dbo.Students", "PriorityType_Id", "dbo.StudentPriorityTypes", "Id");
            RenameTable(name: "dbo.RoomRequestStatus", newName: "RoomTransferStatus");
            RenameTable(name: "dbo.RoomRequests", newName: "RoomTransferRequests");
        }
    }
}
