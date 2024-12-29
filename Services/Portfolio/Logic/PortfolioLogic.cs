using PortfolioService.Data.Entities;
using PortfolioService.Data.Repositories;
using Shared.Events;
using Shared.Messaging;

namespace PortfolioService.Logic
{
    public class PortfolioLogic : IPortfolioLogic
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IEventBus _eventBus;

        public PortfolioLogic(IPortfolioRepository portfolioRepository, IEventBus eventBus)
        {
            _portfolioRepository = portfolioRepository;
            _eventBus = eventBus;
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

        public async Task<bool> DepositFundsAsync(Guid userId, decimal amount)
        {
            // Minimal validation for deposit
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be greater than zero.");
            }

            // Fetch the user's portfolio
            var portfolio = await GetPortfolioByUserIdAsync(userId);
            if (portfolio == null)
            {
                throw new Exception("Portfolio not found.");
            }

            // Update the account balance
            portfolio.AccountBalance += amount;

            // Update the portfolio in the database
            await UpdatePortfolioAsync(portfolio);

            // Publish FundsDepositedEvent to notify other services
            var fundsDepositedEvent = new FundsDepositedEvent
            {
                UserId = userId,
                Amount = amount,
                Timestamp = DateTime.UtcNow
            };

            _eventBus.Publish(fundsDepositedEvent, "PortfolioExchange");

            return true;
        }

        public async Task<bool> WithdrawFundsAsync(Guid userId, decimal amount)
        {
            // Minimal validation for withdrawal
            if (amount <= 0)
            {
                throw new ArgumentException("Withdrawal amount must be greater than zero.");
            }

            // Fetch the user's portfolio
            var portfolio = await GetPortfolioByUserIdAsync(userId);
            if (portfolio == null)
            {
                throw new Exception("Portfolio not found.");
            }

            // Check if the user has sufficient funds
            if (portfolio.AccountBalance < amount)
            {
                throw new Exception("Insufficient funds.");
            }

            // Update the account balance
            portfolio.AccountBalance -= amount;

            // Update the portfolio in the database
            await UpdatePortfolioAsync(portfolio);

            // Publish FundsWithdrawnEvent to notify other services
            var fundsWithdrawnEvent = new FundsWithdrawnEvent
            {
                UserId = userId,
                Amount = amount,
                Timestamp = DateTime.UtcNow
            };

            _eventBus.Publish(fundsWithdrawnEvent, "PortfolioExchange");

            return true;
        }
    }
}
