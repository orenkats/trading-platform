using PortfolioService.Domain.Entities;
using Shared.Persistence;

namespace PortfolioService.Infrastructure.Persistence.Repositories
{
    public interface IPortfolioRepository : IRepository<Portfolio>
    {
        Task<Portfolio?> GetPortfolioByUserIdAsync(Guid userId);
    }
}
