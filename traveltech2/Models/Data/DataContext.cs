using Microsoft.EntityFrameworkCore;

namespace traveltech2.Models.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<App> App { get; set; }
        public DbSet<Head> Head { get; set; }
        public DbSet<Menus> Menus { get; set; }
        public DbSet<MenuItems> MenuItems { get; set; }
        public DbSet<Links> Links { get; set; }
        public DbSet<Footer> Footer { get; set; }
        public DbSet<FooterIcons> FooterIcons { get; set; }
        public DbSet<FooterMenus> FooterMenus { get; set; }
    }
}
