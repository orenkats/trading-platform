using PortfolioService.Data.Entities;
using PortfolioService.Data.Repositories;

namespace PortfolioService.Logic
{
    public class PortfolioLogic : IPortfolioLogic
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioLogic(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task CreatePortfolioAsync(Portfolio portfolio)
        {
            // Use the existing method to create a portfolio
            await _portfolioRepository.AddAsync(portfolio);
        }

        public async Task UpdatePortfolioAsync(Portfolio portfolio)
        {
            // Update an existing portfolio
            await _portfolioRepository.UpdateAsync(portfolio);
        }

        public async Task<Portfolio?> GetPortfolioByUserIdAsync(Guid userId)
        {
            // Fetch the portfolio by UserId
            return await _portfolioRepository.GetPortfolioByUserIdAsync(userId);
        }
    }
}
