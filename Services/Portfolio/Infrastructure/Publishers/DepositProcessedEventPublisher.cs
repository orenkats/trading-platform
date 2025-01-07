using Shared.Events;
using Shared.Messaging;

namespace PortfolioService.Infrastructure.Publishers
{
    public class DepositProcessedEventPublisher
    {
        private readonly IEventBus _eventBus;

        public DepositProcessedEventPublisher(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void PublishDepositProcessed(Guid userId, decimal amount, string status)
        {
            var depositProcessedEvent = new DepositProcessedEvent
            {
                EventId = Guid.NewGuid(),
                UserId = userId,
                Amount = amount,
                Status = status,
                Timestamp = DateTime.UtcNow
            };

            _eventBus.Publish(depositProcessedEvent, "PortfolioExchange");
        }
    }
}
