namespace OrangeAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatedatabasefinal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Commerces", "Phone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Commerces", "Phone", c => c.Int(nullable: false));
        }
    }
}
