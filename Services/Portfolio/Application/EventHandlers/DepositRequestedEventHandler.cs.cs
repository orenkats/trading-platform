using Shared.Events;
using Shared.Messaging;

namespace PortfolioService.Application.EventHandlers
{
    public class DepositRequestedEventHandler : IEventHandler<DepositRequestedEvent>
    {
        private readonly IEventBus _eventBus;

        public DepositRequestedEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task HandleAsync(DepositRequestedEvent depositEvent)
        {
            // Publish DepositRequestedEvent to Payment Exchange
            var depositRequestEvent = new DepositRequestedEvent
            {
                EventId = Guid.NewGuid(),
                UserId = depositEvent.UserId,
                Amount = depositEvent.Amount,
                Status = depositEvent.Status,
                Timestamp = DateTime.UtcNow
            };

            _eventBus.Publish(depositRequestEvent, "PortfolioExchange");

            await Task.CompletedTask;
        }
    }
}
