using PortfolioService.Application.Services;
using Shared.Events;
using Shared.Messaging;
using Microsoft.Extensions.Logging;

namespace PortfolioService.Application.EventHandlers
{
    public class DepositRequestedEventHandler : IEventHandler<DepositRequestedEvent>
    {
        private readonly IPortfolioAppService _portfolioAppService;
        private readonly IEventBus _eventBus;
        private readonly ILogger<DepositRequestedEventHandler> _logger;

        public DepositRequestedEventHandler(
            IPortfolioAppService portfolioAppService,
            IEventBus eventBus,
            ILogger<DepositRequestedEventHandler> logger)
        {
            _portfolioAppService = portfolioAppService;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task HandleAsync(DepositRequestedEvent depositEvent)
        {
            _logger.LogInformation("Handling DepositRequestedEvent for UserId: {UserId}", depositEvent.UserId);

            try
            {
                await _portfolioAppService.DepositFundsAsync(depositEvent.UserId, depositEvent.Amount);

                var transactionEvent = new TransactionCreatedEvent
                {
                    UserId = depositEvent.UserId,
                    PortfolioId = Guid.NewGuid(), // Fetch associated PortfolioId if needed
                    Type = "Deposit",
                    Amount = depositEvent.Amount,
                    Timestamp = DateTime.UtcNow
                };

                _eventBus.Publish(transactionEvent, "PortfolioExchange");
                _logger.LogInformation("Published TransactionCreatedEvent for UserId: {UserId}", depositEvent.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling DepositRequestedEvent for UserId: {UserId}", depositEvent.UserId);
                throw;
            }
        }
    }
}
