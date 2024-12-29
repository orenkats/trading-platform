using Microsoft.EntityFrameworkCore;
using PortfolioService.Domain.Entities;

namespace PortfolioService.Infrastructure.Persistence
{
    public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options) { }

        public DbSet<Portfolio> Portfolios { get; set; } = null!;
        public DbSet<Holding> Holdings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Portfolio entity configuration
            modelBuilder.Entity<Portfolio>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.AccountBalance)
                      .IsRequired()
                      .HasDefaultValue(0.00m); // Default value for new portfolios

                // Define one-to-many relationship with Holdings
                entity.HasMany(e => e.Holdings)
                      .WithOne(h => h.Portfolio) // Specify the navigation property
                      .HasForeignKey(h => h.PortfolioId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Holding entity configuration
            modelBuilder.Entity<Holding>(entity =>
            {
                // Composite key for PortfolioId and StockSymbol
                entity.HasKey(e => new { e.PortfolioId, e.StockSymbol });

                entity.Property(e => e.StockSymbol).IsRequired();
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.AveragePrice).IsRequired();
            });
        }
    }
}
