using Microsoft.EntityFrameworkCore;
using TransactionService.Domain.Entities;
using TransactionService.Domain.Interfaces;
using TransactionService.Infrastructure.DbContexts;

namespace TransactionService.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionDbContext _context;

        public TransactionRepository(TransactionDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction?> GetByIdAsync(Guid id)
        {
            return await _context.Transactions.FindAsync(id);
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task AddAsync(Transaction entity)
        {
            await _context.Transactions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Transaction entity)
        {
            _context.Transactions.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Transactions.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        // Add additional methods for the Transaction repository as needed
        // Example: Get transactions by userId, type, or status
    }
}
