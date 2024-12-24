namespace Shared.Messaging
{
    public interface IEventBus
    {
        void Publish<T>(T @event, string exchangeName) where T : class;
        void Subscribe<T>(string queueName, Func<T, Task> onMessage) where T : class;
    }
}
