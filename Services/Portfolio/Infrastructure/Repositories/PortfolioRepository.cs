using Microsoft.EntityFrameworkCore;
using PortfolioService.Domain.Entities;
using PortfolioService.Domain.Interfaces;
using PortfolioService.Infrastructure.DbContexts;

namespace PortfolioService.Infrastructure.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly PortfolioDbContext _context;

        public PortfolioRepository(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<Portfolio?> GetByIdAsync(Guid id)
        {
            return await _context.Portfolios.FindAsync(id);
        }

        public async Task<IEnumerable<Portfolio>> GetAllAsync()
        {
            return await _context.Portfolios.ToListAsync();
        }

        public async Task AddAsync(Portfolio entity)
        {
            await _context.Portfolios.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Portfolio entity)
        {
            _context.Portfolios.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Portfolios.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Portfolio?> GetPortfolioByUserIdAsync(Guid userId)
        {
            return await _context.Portfolios.FirstOrDefaultAsync(p => p.UserId == userId);
        }
    }
}
