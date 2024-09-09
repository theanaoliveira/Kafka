using Confluent.Kafka;

namespace Kafka.Infrastructure.Interfaces
{
    public interface IProducerTemplate<TKey, TValue>
    {
        Task SendAsync(string topic, TKey key, TValue value);
    }
}
