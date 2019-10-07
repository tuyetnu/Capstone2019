namespace DormyAppService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Equipments", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.RoomTransferRequests", "Id", "dbo.RoomTransferStatus");
            DropForeignKey("dbo.RoomTransferHistories", "Id", "dbo.RoomTransferStatus");
            DropIndex("dbo.Equipments", new[] { "Room_Id" });
            DropPrimaryKey("dbo.RoomTransferStatus");
            AddColumn("dbo.Rooms", "Description", c => c.String());
            AddColumn("dbo.Equipments", "Name", c => c.String(maxLength: 50));
            AddColumn("dbo.Equipments", "Price", c => c.Decimal(nullable: false, storeType: "money"));
            AddColumn("dbo.Equipments", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Equipments", "LastUpdated", c => c.DateTime(nullable: false));
            AddColumn("dbo.EquipmentTypes", "DefaultPrice", c => c.Decimal(nullable: false, storeType: "money"));
            AddColumn("dbo.RoomTypes", "Description", c => c.String());
            AddColumn("dbo.RoomTypes", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.RoomTransferStatus", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Equipments", "Room_Id", c => c.Int());
            AddPrimaryKey("dbo.RoomTransferStatus", "Id");
            CreateIndex("dbo.Equipments", "Room_Id");
            AddForeignKey("dbo.Equipments", "Room_Id", "dbo.Rooms", "Id");
            AddForeignKey("dbo.RoomTransferHistories", "Id", "dbo.RoomTransferStatus", "Id");
            AddForeignKey("dbo.RoomTransferRequests", "Id", "dbo.RoomTransferStatus", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomTransferRequests", "Id", "dbo.RoomTransferStatus");
            DropForeignKey("dbo.RoomTransferHistories", "Id", "dbo.RoomTransferStatus");
            DropForeignKey("dbo.Equipments", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.Equipments", new[] { "Room_Id" });
            DropPrimaryKey("dbo.RoomTransferStatus");
            AlterColumn("dbo.Equipments", "Room_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.RoomTransferStatus", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.RoomTypes", "Price");
            DropColumn("dbo.RoomTypes", "Description");
            DropColumn("dbo.EquipmentTypes", "DefaultPrice");
            DropColumn("dbo.Equipments", "LastUpdated");
            DropColumn("dbo.Equipments", "CreatedDate");
            DropColumn("dbo.Equipments", "Price");
            DropColumn("dbo.Equipments", "Name");
            DropColumn("dbo.Rooms", "Description");
            AddPrimaryKey("dbo.RoomTransferStatus", "Id");
            CreateIndex("dbo.Equipments", "Room_Id");
            AddForeignKey("dbo.RoomTransferHistories", "Id", "dbo.RoomTransferStatus", "Id");
            AddForeignKey("dbo.RoomTransferRequests", "Id", "dbo.RoomTransferStatus", "Id");
            AddForeignKey("dbo.Equipments", "Room_Id", "dbo.Rooms", "Id", cascadeDelete: true);
        }
    }
}
