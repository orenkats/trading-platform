using Microsoft.EntityFrameworkCore;
using PortfolioService.Domain.Entities;
using Shared.Persistence;

namespace PortfolioService.Infrastructure.Persistence.Repositories
{
    public class PortfolioRepository : Repository<Portfolio>, IPortfolioRepository
    {
        public PortfolioRepository(PortfolioDbContext context) : base(context) { }

        public async Task<Portfolio?> GetPortfolioByUserIdAsync(Guid userId)
        {
            return await _context.Set<Portfolio>().FirstOrDefaultAsync(p => p.UserId == userId);
        }
    }
}
