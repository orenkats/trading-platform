using PortfolioService.Domain.Entities;
using PortfolioService.Domain.Exceptions;
using PortfolioService.Domain.Interfaces;
using PortfolioService.Infrastructure.Persistence.Repositories;

namespace PortfolioService.Domain.Services
{
    public class PortfolioDomainService : IPortfolioDomainService
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioDomainService(IPortfolioRepository repository)
        {
            _portfolioRepository = repository;
        }
        public async Task CreatePortfolioAsync(Guid userId)
        {
            // Check if a portfolio already exists for the user
            var existingPortfolio = await _portfolioRepository.GetPortfolioByUserIdAsync(userId);
            if (existingPortfolio != null)
            {
                throw new PortfolioAlreadyExistsException(userId);
            }

            // Create a new portfolio
            var newPortfolio = new Portfolio
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                AccountBalance = 0 // Default balance
            };

            await _portfolioRepository.AddAsync(newPortfolio);
        }
    

        public decimal CalculateUpdatedBalance(decimal currentBalance, decimal amount, bool isDeposit)
        {
            if (amount <= 0)
                throw new InvalidAmountException();

            if (!isDeposit && currentBalance < amount)
                throw new InsufficientFundsException();

            return isDeposit ? currentBalance + amount : currentBalance - amount;
        }

        public async Task AddOrUpdateHoldingAsync(Guid userId, Holding newHolding)
        {
            if (newHolding.Quantity <= 0)
                throw new InvalidHoldingException("Holding quantity must be greater than zero.");

            var portfolio = await _portfolioRepository.GetPortfolioByUserIdAsync(userId)
                ?? throw new PortfolioNotFoundException(userId);

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

            await _portfolioRepository.UpdateAsync(portfolio);
        }

        public async Task DepositFundsAsync(Guid userId, decimal amount)
        {
            if (amount <= 0)
                throw new InvalidAmountException();

            var portfolio = await _portfolioRepository.GetPortfolioByUserIdAsync(userId)
                ?? throw new PortfolioNotFoundException(userId);

            portfolio.AccountBalance = CalculateUpdatedBalance(portfolio.AccountBalance, amount, isDeposit: true);

            await _portfolioRepository.UpdateAsync(portfolio);
        }

        public async Task WithdrawFundsAsync(Guid userId, decimal amount)
        {
            if (amount <= 0)
                throw new InvalidAmountException();

            var portfolio = await _portfolioRepository.GetPortfolioByUserIdAsync(userId)
                ?? throw new PortfolioNotFoundException(userId);

            portfolio.AccountBalance = CalculateUpdatedBalance(portfolio.AccountBalance, amount, isDeposit: false);

            await _portfolioRepository.UpdateAsync(portfolio);
        }
    }
}
