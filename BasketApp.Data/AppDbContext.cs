using BasketApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);//for configurations..

            base.OnModelCreating(modelBuilder);
        }
    }
}
