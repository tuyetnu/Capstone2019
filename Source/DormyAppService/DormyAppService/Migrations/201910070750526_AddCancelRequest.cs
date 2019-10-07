namespace DormyAppService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCancelRequest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CancelContractRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                        CancelationDate = c.DateTime(nullable: false),
                        Reason = c.String(nullable: false, maxLength: 500),
                        Staff_Id = c.String(maxLength: 128),
                        Student_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Staffs", t => t.Staff_Id)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .Index(t => t.Staff_Id)
                .Index(t => t.Student_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CancelContractRequests", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.CancelContractRequests", "Staff_Id", "dbo.Staffs");
            DropIndex("dbo.CancelContractRequests", new[] { "Student_Id" });
            DropIndex("dbo.CancelContractRequests", new[] { "Staff_Id" });
            DropTable("dbo.CancelContractRequests");
        }
    }
}
