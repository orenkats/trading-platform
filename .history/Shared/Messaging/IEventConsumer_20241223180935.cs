namespace Shared.Messaging
{
    public interface IEventConsumer
    {
        void Subscribe<T>(string queueName, Action<T> onMessageReceived) where T : class;
    }
}
