using System.Threading.Tasks;

namespace Shared.Messaging.Interfaces
{
    public interface IMessageProcessor
    {
        Task ProcessAsync(byte[] rawMessage, string queueName);
    }
}
