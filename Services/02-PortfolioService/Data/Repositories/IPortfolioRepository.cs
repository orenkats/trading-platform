using PortfolioService.Data.Entities;
using Shared.Persistence;

namespace PortfolioService.Data.Repositories
{
    public interface IPortfolioRepository : IRepository<Portfolio>
    {
        // Add any portfolio-specific methods here
    }
}
