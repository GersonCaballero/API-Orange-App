﻿using System;
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

        public System.Data.Entity.DbSet<OrangeAPI.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<OrangeAPI.Models.Commerce> Commerces { get; set; }

        public System.Data.Entity.DbSet<OrangeAPI.Models.TypeOfUser> TypeOfUsers { get; set; }

        public System.Data.Entity.DbSet<OrangeAPI.Models.UserAdmin> UserAdmins { get; set; }

        public System.Data.Entity.DbSet<OrangeAPI.Models.EndUser> EndUsers { get; set; }
    }
}
