namespace OrangeAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bikers",
                c => new
                    {
                        IdBiker = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Telephone = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Age = c.String(nullable: false),
                        IdUserType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdBiker)
                .ForeignKey("dbo.TypeOfUsers", t => t.IdUserType, cascadeDelete: true)
                .Index(t => t.IdUserType);
            
            CreateTable(
                "dbo.TypeOfUsers",
                c => new
                    {
                        IdUserType = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IdUserType);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        IdCategory = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Image = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.IdCategory);
            
            CreateTable(
                "dbo.Commerces",
                c => new
                    {
                        IdCommerce = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        Phone = c.Int(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        RTN = c.String(nullable: false),
                        Image = c.Byte(nullable: false),
                        IdCategory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdCommerce)
                .ForeignKey("dbo.Categories", t => t.IdCategory, cascadeDelete: true)
                .Index(t => t.IdCategory);
            
            CreateTable(
                "dbo.EndUsers",
                c => new
                    {
                        IdEndUser = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 45),
                        Phone = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        IdUserType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdEndUser)
                .ForeignKey("dbo.TypeOfUsers", t => t.IdUserType, cascadeDelete: true)
                .Index(t => t.IdUserType);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        IdProduct = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Image = c.Byte(nullable: false),
                        Precio = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        IdCommerce = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdProduct)
                .ForeignKey("dbo.Commerces", t => t.IdCommerce, cascadeDelete: true)
                .Index(t => t.IdCommerce);
            
            CreateTable(
                "dbo.UserAdmins",
                c => new
                    {
                        IdAdmin = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        IdUserType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdAdmin)
                .ForeignKey("dbo.TypeOfUsers", t => t.IdUserType, cascadeDelete: true)
                .Index(t => t.IdUserType);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAdmins", "IdUserType", "dbo.TypeOfUsers");
            DropForeignKey("dbo.Products", "IdCommerce", "dbo.Commerces");
            DropForeignKey("dbo.EndUsers", "IdUserType", "dbo.TypeOfUsers");
            DropForeignKey("dbo.Commerces", "IdCategory", "dbo.Categories");
            DropForeignKey("dbo.Bikers", "IdUserType", "dbo.TypeOfUsers");
            DropIndex("dbo.UserAdmins", new[] { "IdUserType" });
            DropIndex("dbo.Products", new[] { "IdCommerce" });
            DropIndex("dbo.EndUsers", new[] { "IdUserType" });
            DropIndex("dbo.Commerces", new[] { "IdCategory" });
            DropIndex("dbo.Bikers", new[] { "IdUserType" });
            DropTable("dbo.UserAdmins");
            DropTable("dbo.Products");
            DropTable("dbo.EndUsers");
            DropTable("dbo.Commerces");
            DropTable("dbo.Categories");
            DropTable("dbo.TypeOfUsers");
            DropTable("dbo.Bikers");
        }
    }
}
