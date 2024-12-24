using Microsoft.EntityFrameworkCore;
using PortfolioService.Data.Entities;

namespace PortfolioService.Data
{
    public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options) { }

        public DbSet<Portfolio> Portfolios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Portfolio>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.HasMany(e => e.Holdings).WithOne().OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Holding>(entity =>
            {
                entity.HasKey(e => new { e.StockSymbol });
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.AveragePrice).IsRequired();
            });
        }
    }
}
