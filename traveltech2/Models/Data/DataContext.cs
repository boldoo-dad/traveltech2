using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using traveltech2.Models.HeadModels;

namespace traveltech2.Models.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<App> Apps { get; set; }
        public DbSet<Head> Heads { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Drop> Drops { get; set; }
        public DbSet<Tool> MyProperty { get; set; }
    }
}
