using Shared.Events;
using Shared.Messaging;

namespace PortfolioService.Infrastructure.Publishers
{
    public class DepositProcessedEventPublisher
    {
        private readonly IEventPublisher _eventPublisher;

        public DepositProcessedEventPublisher(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
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

            _eventPublisher.Publish(depositProcessedEvent, "PortfolioExchange");
        }
    }
}
