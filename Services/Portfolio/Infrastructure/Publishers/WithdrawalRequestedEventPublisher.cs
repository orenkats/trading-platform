using Shared.Events;
using Shared.Messaging;

namespace PortfolioService.Infrastructure.Publishers
{
    public class WithdrawalRequestedEventPublisher
    {
        private readonly IEventPublisher _eventPublisher;

        public WithdrawalRequestedEventPublisher(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public void PublishWithdrawalRequested(Guid userId, decimal amount)
        {
            var withdrawalRequestedEvent = new WithdrawalRequestedEvent
            {
                EventId = Guid.NewGuid(),
                UserId = userId,
                Amount = amount,
                Timestamp = DateTime.UtcNow
            };

            _eventPublisher.Publish(withdrawalRequestedEvent, "PortfolioExchange");
        }
    }
}
