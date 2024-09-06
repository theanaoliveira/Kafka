namespace Kafka.Infrastructure.Interfaces
{
    public interface IKafkaTemplate<TKey, TValue>
    {
        Task SendAsync(string topic, TKey key, TValue value);
    }
}
