using Shared.Events;
using Shared.Messaging;

namespace PortfolioService.Infrastructure.Publishers
{
    public class WithdrawalProcessedEventPublisher
    {
        private readonly IEventBus _eventBus;

        public WithdrawalProcessedEventPublisher(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void PublishWithdrawalProcessed(Guid userId, decimal amount, string status)
        {
            var withdrawalProcessedEvent = new WithdrawalProcessedEvent
            {
                EventId = Guid.NewGuid(),
                UserId = userId,
                Amount = amount,
                Status = status,
                Timestamp = DateTime.UtcNow
            };

            _eventBus.Publish(withdrawalProcessedEvent, "PortfolioExchange");
        }
    }
}
