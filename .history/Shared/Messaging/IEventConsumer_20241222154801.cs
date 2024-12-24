namespace Shared.Messaging
{
    public interface IEventConsumer
    {
        void Subscribe<T>(string queueName, Func<T, Task> onMessage) where T : class;
    }
}
