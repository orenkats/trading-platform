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

        public async Task AddOrUpdateHoldingAsync(Guid userId, Holding holding)
        {
            // Fetch the portfolio for the user
            var portfolio = await GetPortfolioByUserIdAsync(userId);
            if (portfolio == null)
            {
                // If no portfolio exists, create one
                portfolio = new Portfolio
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    AccountBalance = 0
                };

                await CreatePortfolioAsync(portfolio);
            }

            // Check if the holding already exists
            var existingHolding = portfolio.Holdings.FirstOrDefault(h => h.StockSymbol == holding.StockSymbol);
            if (existingHolding != null)
            {
                // Update existing holding
                var totalCost = existingHolding.Quantity * existingHolding.AveragePrice + holding.Quantity * holding.AveragePrice;
                existingHolding.Quantity += holding.Quantity;
                existingHolding.AveragePrice = totalCost / existingHolding.Quantity;
            }
            else
            {
                // Add the new holding
                portfolio.Holdings.Add(holding);
            }

            // Update the portfolio with the new/updated holdings
            await UpdatePortfolioAsync(portfolio);
        }
    }
}
