using PortfolioService.Domain.Entities;

namespace PortfolioService.Domain.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<Portfolio?> GetByIdAsync(Guid id);
        Task<IEnumerable<Portfolio>> GetAllAsync();
        Task AddAsync(Portfolio entity);
        Task UpdateAsync(Portfolio entity);
        Task DeleteAsync(Guid id);
        Task<Portfolio?> GetPortfolioByUserIdAsync(Guid userId);
    }
}
