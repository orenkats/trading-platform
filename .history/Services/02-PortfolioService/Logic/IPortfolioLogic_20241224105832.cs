using PortfolioService.Data.Entities;

namespace PortfolioService.Logic
{
    public interface IPortfolioLogic
    {
        Task AddPortfolioAsync(Portfolio portfolio);
        Task UpdatePortfolioAsync(Portfolio portfolio);
    }
}
