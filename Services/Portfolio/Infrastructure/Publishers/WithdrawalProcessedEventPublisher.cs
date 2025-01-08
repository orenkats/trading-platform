using Shared.Events;
using Shared.Messaging;

namespace PortfolioService.Infrastructure.Publishers
{
    public class WithdrawalProcessedEventPublisher
    {
        private readonly IEventPublisher _eventPublisher;

        public WithdrawalProcessedEventPublisher(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
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

            _eventPublisher.Publish(withdrawalProcessedEvent, "PortfolioExchange");
        }
    }
}
