namespace Shared.Messaging.Interfaces
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event, string exchangeName) where T : class;
    }
}
