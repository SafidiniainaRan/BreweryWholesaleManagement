using BreweryWholesaleManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesaleManagement.Data
{
    public class BreweryContext : DbContext
    {
        public BreweryContext(DbContextOptions<BreweryContext> options) : base(options) { }

        public DbSet<Brewer> Brewers { get; set; }
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Wholesaler> Wholesalers { get; set; }
        public DbSet<WholesalerStock> WholesalerStocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brewer>().ToTable("Brewer");
            modelBuilder.Entity<Beer>().ToTable("Beers");
            modelBuilder.Entity<Wholesaler>().ToTable("Wolesaler");
            modelBuilder.Entity<WholesalerStock>().ToTable("WolesalerStock");
        }
    }
}
