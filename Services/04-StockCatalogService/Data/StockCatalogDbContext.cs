using Microsoft.EntityFrameworkCore;
using StockCatalogService.Data.Entities;

namespace StockCatalogService.Data
{
    public class StockCatalogDbContext : DbContext
    {
        public StockCatalogDbContext(DbContextOptions<StockCatalogDbContext> options) : base(options) { }

        public DbSet<StockCatalog> StockCatalogs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StockCatalog>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StockSymbol).IsRequired();
            });
        }
    }
}
