using ETicaret.Core.Entities;
using ETicaret.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ETicaret.Data
{
    public  class DatabaseContext : DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-FUO4J03; Database=ETicaretDb; Trusted_Connection=true; 
            TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
