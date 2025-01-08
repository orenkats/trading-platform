using System;

namespace Shared.Messaging.Interfaces
{
    public interface IEventResolver
    {
        Type? Resolve(string queueName);
    }
}
