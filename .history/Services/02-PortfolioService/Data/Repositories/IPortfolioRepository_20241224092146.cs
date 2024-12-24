using PortfolioService.Data.Entities;
using Shared.Persistence;

namespace PortfolioService.Data.Repositories
{
    public interface IPortfolioRepository : IRepository<Portfolio>
    {
        // Additional portfolio-specific methods can be declared here if needed.
        Task<Portfolio?> GetPortfolioByUserAndStockAsync(Guid userId, string stockSymbol);
        Task<IEnumerable<Portfolio>> GetPortfoliosByUserAsync(Guid userId);
    }
}
