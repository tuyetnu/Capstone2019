namespace DormyAppService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudentToNotification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "Student_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Notifications", "Student_Id");
            AddForeignKey("dbo.Notifications", "Student_Id", "dbo.Students", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "Student_Id", "dbo.Students");
            DropIndex("dbo.Notifications", new[] { "Student_Id" });
            DropColumn("dbo.Notifications", "Student_Id");
        }
    }
}
