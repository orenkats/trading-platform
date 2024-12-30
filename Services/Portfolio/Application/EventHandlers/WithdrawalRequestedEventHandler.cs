using PortfolioService.Application.Services;
using Shared.Events;
using Shared.Messaging;
using Microsoft.Extensions.Logging;

namespace PortfolioService.Application.EventHandlers
{
    public class WithdrawalRequestedEventHandler : IEventHandler<WithdrawalRequestedEvent>
    {
        private readonly IPortfolioAppService _portfolioAppService;
        private readonly IEventBus _eventBus;
        private readonly ILogger<WithdrawalRequestedEventHandler> _logger;

        public WithdrawalRequestedEventHandler(
            IPortfolioAppService portfolioAppService,
            IEventBus eventBus,
            ILogger<WithdrawalRequestedEventHandler> logger)
        {
            _portfolioAppService = portfolioAppService;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task HandleAsync(WithdrawalRequestedEvent withdrawalEvent)
        {
            _logger.LogInformation("Handling WithdrawalRequestedEvent for UserId: {UserId}", withdrawalEvent.UserId);

            try
            {
                await _portfolioAppService.WithdrawFundsAsync(withdrawalEvent.UserId, withdrawalEvent.Amount);

                var transactionEvent = new TransactionCreatedEvent
                {
                    UserId = withdrawalEvent.UserId,
                    PortfolioId = Guid.NewGuid(), // Fetch associated PortfolioId if needed
                    Type = "Withdrawal",
                    Amount = withdrawalEvent.Amount,
                    Timestamp = DateTime.UtcNow
                };

                _eventBus.Publish(transactionEvent, "PortfolioExchange");
                _logger.LogInformation("Published TransactionCreatedEvent for UserId: {UserId}", withdrawalEvent.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling WithdrawalRequestedEvent for UserId: {UserId}", withdrawalEvent.UserId);
                throw;
            }
        }
    }
}
