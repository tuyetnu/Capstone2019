namespace DormyAppService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RoomTransferHistories", "RoomTransferRequest_Id", "dbo.RoomTransferRequests");
            DropForeignKey("dbo.RoomTransferHistories", "Staff_Id", "dbo.Staffs");
            DropForeignKey("dbo.RoomTransferHistories", "Id", "dbo.RoomTransferStatus");
            DropForeignKey("dbo.EvaluationScoreHistories", "CreatedBy_Id", "dbo.Staffs");
            DropForeignKey("dbo.EvaluationScoreHistories", "TargetStudent_Id", "dbo.Students");
            DropForeignKey("dbo.RoomTransferHistories", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.RoomTransferRequests", "Id", "dbo.RoomTransferStatus");
            DropIndex("dbo.RoomTransferRequests", new[] { "Id" });
            DropIndex("dbo.RoomTransferHistories", new[] { "Id" });
            DropIndex("dbo.RoomTransferHistories", new[] { "RoomTransferRequest_Id" });
            DropIndex("dbo.RoomTransferHistories", new[] { "Staff_Id" });
            DropIndex("dbo.RoomTransferHistories", new[] { "Student_Id" });
            DropIndex("dbo.EvaluationScoreHistories", new[] { "CreatedBy_Id" });
            DropIndex("dbo.EvaluationScoreHistories", new[] { "TargetStudent_Id" });
            DropPrimaryKey("dbo.RoomTransferRequests");
            AddColumn("dbo.RoomTransferRequests", "Status_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.RoomTransferRequests", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.RoomTransferRequests", "Id");
            CreateIndex("dbo.RoomTransferRequests", "Status_Id");
            AddForeignKey("dbo.RoomTransferRequests", "Status_Id", "dbo.RoomTransferStatus", "Id", cascadeDelete: true);
            DropTable("dbo.RoomTransferHistories");
            DropTable("dbo.EvaluationScoreHistories");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.RoomTransferRequests", "Status_Id", "dbo.RoomTransferStatus");
            DropIndex("dbo.RoomTransferRequests", new[] { "Status_Id" });
            DropPrimaryKey("dbo.RoomTransferRequests");
            AlterColumn("dbo.RoomTransferRequests", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.RoomTransferRequests", "Status_Id");
            AddPrimaryKey("dbo.RoomTransferRequests", "Id");
            CreateIndex("dbo.EvaluationScoreHistories", "TargetStudent_Id");
            CreateIndex("dbo.EvaluationScoreHistories", "CreatedBy_Id");
            CreateIndex("dbo.RoomTransferHistories", "Student_Id");
            CreateIndex("dbo.RoomTransferHistories", "Staff_Id");
            CreateIndex("dbo.RoomTransferHistories", "RoomTransferRequest_Id");
            CreateIndex("dbo.RoomTransferHistories", "Id");
            CreateIndex("dbo.RoomTransferRequests", "Id");
            AddForeignKey("dbo.RoomTransferRequests", "Id", "dbo.RoomTransferStatus", "Id");
            AddForeignKey("dbo.RoomTransferHistories", "Student_Id", "dbo.Students", "Id");
            AddForeignKey("dbo.EvaluationScoreHistories", "TargetStudent_Id", "dbo.Students", "Id");
            AddForeignKey("dbo.EvaluationScoreHistories", "CreatedBy_Id", "dbo.Staffs", "Id");
            AddForeignKey("dbo.RoomTransferHistories", "Id", "dbo.RoomTransferStatus", "Id");
            AddForeignKey("dbo.RoomTransferHistories", "Staff_Id", "dbo.Staffs", "Id");
            AddForeignKey("dbo.RoomTransferHistories", "RoomTransferRequest_Id", "dbo.RoomTransferRequests", "Id", cascadeDelete: true);
        }
    }
}
