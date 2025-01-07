using Shared.Events;
using Shared.Messaging;

namespace PortfolioService.Domain.Producers
{
    public class FundsDepositedEventProducer
    {
        private readonly IEventBus _eventBus;

        public FundsDepositedEventProducer(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void PublishFundsDeposited(Guid userId, decimal UpdatedBalance)
        {
            var eventMessage = new DepositCompletedEvent
            {
                UserId = userId,
                UpdatedBalance = UpdatedBalance,
                Timestamp = DateTime.UtcNow
            };

            _eventBus.Publish(eventMessage, "PortfolioExchange");
        }
    }
}
