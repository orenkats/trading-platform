using PortfolioService.Data.Entities;

namespace PortfolioService.Logic
{
    public interface IPortfolioLogic
    {
        Task CreatePortfolioAsync(Portfolio portfolio); // Use the existing method
        Task UpdatePortfolioAsync(Portfolio portfolio);
        Task<Portfolio?> GetPortfolioByUserIdAsync(Guid userId);
    }
}
