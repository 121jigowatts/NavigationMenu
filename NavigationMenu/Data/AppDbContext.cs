using NavigationMenu.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NavigationMenu.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("AppDbContext")
        {

        }

        public virtual DbSet<MenuItem> MenuItems { get; set; }
    }
}