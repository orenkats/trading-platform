using Shared.Events;
using Shared.Messaging;

namespace PortfolioService.Infrastructure.Publishers
{
    public class DepositRequestedEventPublisher
    {
        private readonly IEventPublisher _eventPublisher;

        public DepositRequestedEventPublisher(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
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

            _eventPublisher.Publish(depositRequestedEvent, "PortfolioExchange");
        }
    }
}
