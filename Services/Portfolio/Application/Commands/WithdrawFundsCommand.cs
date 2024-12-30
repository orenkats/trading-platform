using PortfolioService.Infrastructure.Persistence.Repositories;
using PortfolioService.Domain.Interfaces;
using Shared.Messaging;
using Shared.Events;

namespace PortfolioService.Application.Commands
{
    public class WithdrawFundsCommand
    {
        private readonly Guid _userId;
        private readonly decimal _amount;
        private readonly IPortfolioDomainService _domainService;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IEventBus _eventBus;

        public WithdrawFundsCommand(
            Guid userId,
            decimal amount,
            IPortfolioDomainService domainService,
            IPortfolioRepository portfolioRepository,
            IEventBus eventBus)
        {
            _userId = userId;
            _amount = amount;
            _domainService = domainService;
            _portfolioRepository = portfolioRepository;
            _eventBus = eventBus;
        }

        public async Task ExecuteAsync()
        {
            // Fetch the portfolio
            var portfolio = await _portfolioRepository.GetPortfolioByUserIdAsync(_userId)
                ?? throw new Exception("Portfolio not found for the user.");

            // Perform the withdrawal using the domain service
            await _domainService.WithdrawFundsAsync(_userId, _amount);

            // Publish the transaction event
            var transactionEvent = new TransactionCreatedEvent
            {
                UserId = _userId,
                PortfolioId = portfolio.Id,
                Type = "Withdrawal",
                Amount = _amount,
                Timestamp = DateTime.UtcNow
            };

            _eventBus.Publish(transactionEvent, "PortfolioExchange");
        }
    }
}
