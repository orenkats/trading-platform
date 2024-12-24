namespace Shared.Messaging
{
    public interface IEventConsumer
    {
        // Updated to accept an asynchronous event handler
        void Subscribe<T>(string queueName, Func<T, Task> onMessageReceived) where T : class;
    }
}
