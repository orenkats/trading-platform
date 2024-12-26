using Microsoft.EntityFrameworkCore;
using TransactionsService.Data.

namespace TransactionsService.Data
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options) { }

        // Define the DbSet for transactions
        public DbSet<Transaction> Transactions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Transaction entity
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(t => t.Id);  // Define the primary key

                // Configure properties
                entity.Property(t => t.TransactionType)
                    .IsRequired()
                    .HasMaxLength(50);  // Limiting the length of TransactionType

                entity.Property(t => t.Status)
                    .IsRequired()
                    .HasMaxLength(50);  // Limiting the length of Status
            });
        }
    }
}
