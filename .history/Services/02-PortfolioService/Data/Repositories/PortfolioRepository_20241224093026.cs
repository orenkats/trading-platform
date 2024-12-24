using Microsoft.EntityFrameworkCore;
using PortfolioService.Data.Entities;
using Shared.Persistence;

namespace PortfolioService.Data.Repositories
{
    public class PortfolioRepository : Repository<Portfolio>, IPortfolioRepository
    {
        private readonly DbContext _context;

        public PortfolioRepository(DbContext context) : base(context)
        {
            _context = context;
        }

        // Get a specific portfolio entry by user and stock
        public async Task<Portfolio?> GetPortfolioByUserAndStockAsync(Guid userId, string stockSymbol)
        {
            return await _context.Set<Portfolio>()
                .FirstOrDefaultAsync(p => p.UserId == userId && p.StockSymbol == stockSymbol);
        }

        // Get all portfolio entries for a user
        public async Task<IEnumerable<Portfolio>> GetPortfoliosByUserAsync(Guid userId)
        {
            return await _context.Set<Portfolio>()
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }
    }
}
