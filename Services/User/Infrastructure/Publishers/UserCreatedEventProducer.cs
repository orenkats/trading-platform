using Shared.Events;
using Shared.Messaging;

namespace UserService.Application.Publishers
{
    public class DepositProcessedEventProducer
    {
        private readonly IEventBus _eventBus;

        public DepositProcessedEventProducer(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void PublishUserCreated(Guid userId, string name, string email)
        {
            var UserCreatedEvent = new UserCreatedEvent
            {
                EventId = Guid.NewGuid(),
                UserId = userId,
                Name = name,
                Email = email,
                Timestamp = DateTime.UtcNow
            };

            _eventBus.Publish(UserCreatedEvent, "UserExchange");
        }
    }
}
