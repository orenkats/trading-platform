using Microsoft.EntityFrameworkCore;
using PortfolioService.Data.Entities;

namespace PortfolioService.Data
{
    public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options) { }

        public DbSet<Portfolio> Portfolios { get; set; } = null!;
        public DbSet<Holding> Holdings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Portfolio>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(e => e.Holdings).WithOne().HasForeignKey(h => h.Id).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Holding>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StockSymbol).IsRequired();
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.AveragePrice).IsRequired();
            });
        }
    }
}
