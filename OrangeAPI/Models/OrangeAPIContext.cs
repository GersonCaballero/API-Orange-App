using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OrangeAPI.Models
{
    public class OrangeAPIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public OrangeAPIContext() : base("name=OrangeAPIContext")
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Commerce> Commerces { get; set; }

        public DbSet<TypeOfUser> TypeOfUsers { get; set; }

        public DbSet<UserAdmin> UserAdmins { get; set; }

        public DbSet<EndUser> EndUsers { get; set; }

        public DbSet<Biker> Bikers { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}
