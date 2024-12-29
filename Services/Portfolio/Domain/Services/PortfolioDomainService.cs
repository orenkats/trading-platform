using PortfolioService.Domain.Entities;
using PortfolioService.Domain.Exceptions;
using PortfolioService.Domain.Interfaces;
using PortfolioService.Infrastructure.Persistence.Repositories;

namespace PortfolioService.Domain.Services
{
    public class PortfolioDomainService : IPortfolioDomainService
    {
        private readonly IPortfolioRepository _repository;

        public PortfolioDomainService(IPortfolioRepository repository)
        {
            _repository = repository;
        }

        public decimal CalculateUpdatedBalance(decimal currentBalance, decimal amount, bool isDeposit)
        {
            if (isDeposit)
            {
                return currentBalance + amount;
            }

            if (currentBalance < amount)
            {
                throw new InsufficientFundsException("Insufficient funds for withdrawal.");
            }

            return currentBalance - amount;
        }

        public async Task AddOrUpdateHoldingAsync(Guid userId, Holding newHolding)
        {
            var portfolio = await _repository.GetPortfolioByUserIdAsync(userId)
                ?? throw new Exception("Portfolio not found.");

            var existingHolding = portfolio.Holdings
                .FirstOrDefault(h => h.StockSymbol == newHolding.StockSymbol);

            if (existingHolding != null)
            {
                // Update existing holding
                var totalCost = (existingHolding.Quantity * existingHolding.AveragePrice) +
                                (newHolding.Quantity * newHolding.AveragePrice);
                existingHolding.Quantity += newHolding.Quantity;
                existingHolding.AveragePrice = totalCost / existingHolding.Quantity;
            }
            else
            {
                // Add new holding
                portfolio.Holdings.Add(newHolding);
            }

            await _repository.UpdateAsync(portfolio);
        }

        public async Task DepositFundsAsync(Guid userId, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be greater than zero.");

            var portfolio = await _repository.GetPortfolioByUserIdAsync(userId)
                ?? throw new Exception("Portfolio not found.");

            portfolio.AccountBalance = CalculateUpdatedBalance(portfolio.AccountBalance, amount, isDeposit: true);

            await _repository.UpdateAsync(portfolio);
        }

        public async Task WithdrawFundsAsync(Guid userId, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be greater than zero.");

            var portfolio = await _repository.GetPortfolioByUserIdAsync(userId)
                ?? throw new Exception("Portfolio not found.");

            portfolio.AccountBalance = CalculateUpdatedBalance(portfolio.AccountBalance, amount, isDeposit: false);

            await _repository.UpdateAsync(portfolio);
        }

    }
}
