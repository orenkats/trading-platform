using PortfolioService.Application.Queries;
using Shared.Events;
using Shared.Messaging;

namespace PortfolioService.Application.EventHandlers
{
    public class WithdrawalRequestedEventHandler : IEventHandler<WithdrawalRequestedEvent>
    {
        private readonly GetAccountBalanceQuery _getAccountBalanceQuery;
        private readonly IEventBus _eventBus;

        public WithdrawalRequestedEventHandler(
            GetAccountBalanceQuery getAccountBalanceQuery,
            IEventBus eventBus)
        {
            _getAccountBalanceQuery = getAccountBalanceQuery;
            _eventBus = eventBus;
        }

        public async Task HandleAsync(WithdrawalRequestedEvent withdrawalEvent)
        {
            // Check balance using the query
            var balance = await _getAccountBalanceQuery.ExecuteAsync(withdrawalEvent.UserId);

            if (balance < withdrawalEvent.Amount)
            {
                throw new Exception("Insufficient balance for withdrawal.");
            }

            // Publish WithdrawalRequestedEvent to Payment Exchange
            var withdrawalRequestEvent = new WithdrawalRequestedEvent
            {
                EventId = Guid.NewGuid(),
                UserId = withdrawalEvent.UserId,
                Amount = withdrawalEvent.Amount,
                Status = "Requested",
                Timestamp = DateTime.UtcNow
            };

            _eventBus.Publish(withdrawalRequestEvent, "PortfolioExchange");
        }
    }
}
