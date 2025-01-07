using Shared.Events;
using Shared.Messaging;

namespace PortfolioService.Application.EventProducers
{
    public class DepositProcessedEventProducer
    {
        private readonly IEventBus _eventBus;

        public DepositProcessedEventProducer(IEventBus eventBus)
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
