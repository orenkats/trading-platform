using PortfolioService.Domain.Entities;
using PortfolioService.Domain.Interfaces;
using PortfolioService.Infrastructure.Persistence.Repositories;

namespace PortfolioService.Application.Commands
{
    public class WithdrawFundsCommand
    {
        private readonly Guid _userId;
        private readonly decimal _amount;
        private readonly IPortfolioRepository _repository;
        private readonly IPortfolioDomainService _domainService;

        public WithdrawFundsCommand(Guid userId, decimal amount, IPortfolioRepository repository, IPortfolioDomainService domainService)
        {
            _userId = userId;
            _amount = amount;
            _repository = repository;
            _domainService = domainService;
        }

        public async Task ExecuteAsync()
        {
            if (_amount <= 0)
                throw new ArgumentException("Withdrawal amount must be greater than zero.");

            var portfolio = await _repository.GetPortfolioByUserIdAsync(_userId)
                ?? throw new Exception("Portfolio not found.");

            // Delegate business logic to domain service
            portfolio.AccountBalance = _domainService.CalculateUpdatedBalance(portfolio.AccountBalance, _amount, isDeposit: false);

            // Persist the updated portfolio
            await _repository.UpdateAsync(portfolio);
        }
    }
}
