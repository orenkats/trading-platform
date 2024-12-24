using PortfolioService.Data.Entities;

namespace PortfolioService.Logic
{
    public interface IPortfolioLogic
    {
        Task<Portfolio> CreatePortfolioForUserAsync(Guid userId);
    }
}

