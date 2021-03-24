using Microsoft.EntityFrameworkCore;

namespace traveltech2.Models.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<App> Apps { get; set; }
        public DbSet<Head> Heads { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Drop> Drops { get; set; }
        public DbSet<Logo> Logos { get; set; }
    }
}
