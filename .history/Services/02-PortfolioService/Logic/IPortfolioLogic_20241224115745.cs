using PortfolioService.Data.Entities;

namespace PortfolioService.Logic
{
    public interface IPortfolioLogic
    {
        Task CreatePortfolioAsync(Portfolio portfolio); // Updated name
        Task UpdatePortfolioAsync(Portfolio portfolio);
    }
}
