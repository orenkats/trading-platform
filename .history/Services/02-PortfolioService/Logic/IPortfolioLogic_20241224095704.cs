using PortfolioService.Data.Entities;

namespace PortfolioService.Logic
{
    public interface IPortfolioLogic
    {
        Task CreatePortfolioForUserAsync(Guid userId);
    }
}
