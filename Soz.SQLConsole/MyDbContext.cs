using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Soz.SQLConsole
{
    public class MyDBContext : DbContext
    {
        public MyDBContext() : base("DbConnectionString")
        {
        }

        public DbSet<UserManager> UserManagers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}