using Shared.Events;
using Shared.Messaging;

namespace PortfolioService.Infrastructure.Publishers
{
    public class DepositRequestedEventPublisher
    {
        private readonly IEventBus _eventBus;

        public DepositRequestedEventPublisher(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void PublishDepositRequested(Guid userId, decimal amount)
        {
            var depositRequestedEvent = new DepositRequestedEvent
            {
                EventId = Guid.NewGuid(),
                UserId = userId,
                Amount = amount,
                Timestamp = DateTime.UtcNow
            };

            _eventBus.Publish(depositRequestedEvent, "PortfolioExchange");
        }
    }
}
