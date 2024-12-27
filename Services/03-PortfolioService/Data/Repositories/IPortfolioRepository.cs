using PortfolioService.Data.Entities;
using Shared.Persistence;

namespace PortfolioService.Data.Repositories
{
    public interface IPortfolioRepository : IRepository<Portfolio>
    {
        Task<Portfolio?> GetPortfolioByUserIdAsync(Guid userId);
    }
}
