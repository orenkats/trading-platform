using Shared.Events;
using Shared.Messaging;

namespace PortfolioService.Infrastructure.Publishers
{
    public class WithdrawalRequestedEventPublisher
    {
        private readonly IEventBus _eventBus;

        public WithdrawalRequestedEventPublisher(IEventBus eventBus)
        {
            _eventBus = eventBus;
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

            _eventBus.Publish(withdrawalRequestedEvent, "PortfolioExchange");
        }
    }
}
