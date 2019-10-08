namespace DormyAppService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Gender", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "Address", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "Address");
            DropColumn("dbo.Students", "Gender");
        }
    }
}
