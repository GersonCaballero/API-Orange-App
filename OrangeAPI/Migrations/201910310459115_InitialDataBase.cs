namespace OrangeAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDataBase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bikers", "State", c => c.Boolean(nullable: false));
            AddColumn("dbo.Categories", "State", c => c.Boolean(nullable: false));
            AddColumn("dbo.Commerces", "State", c => c.Boolean(nullable: false));
            AddColumn("dbo.EndUsers", "State", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "State", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserAdmins", "State", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserAdmins", "State");
            DropColumn("dbo.Products", "State");
            DropColumn("dbo.EndUsers", "State");
            DropColumn("dbo.Commerces", "State");
            DropColumn("dbo.Categories", "State");
            DropColumn("dbo.Bikers", "State");
        }
    }
}
