using Shared.Events;
using Shared.Messaging;

namespace PortfolioService.Infrastructure.Publishers
{
    public class FundsDepositedEventProducer
    {
        private readonly IEventPublisher _eventPublisher;

        public FundsDepositedEventProducer(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public void PublishFundsDeposited(Guid userId, decimal UpdatedBalance)
        {
            var eventMessage = new DepositCompletedEvent
            {
                UserId = userId,
                UpdatedBalance = UpdatedBalance,
                Timestamp = DateTime.UtcNow
            };

            _eventPublisher.Publish(eventMessage, "PortfolioExchange");
        }
    }
}
