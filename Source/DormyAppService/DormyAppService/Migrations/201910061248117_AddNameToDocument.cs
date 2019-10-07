namespace DormyAppService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameToDocument : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "Name");
        }
    }
}
