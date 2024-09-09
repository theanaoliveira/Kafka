using Confluent.Kafka;
using Kafka.Infrastructure.Interfaces;

namespace Kafka.Infrastructure.Producers
{
    public class ProducerTemplate<TKey, TValue> : IProducerTemplate<TKey, TValue>
    {
        private readonly IProducer<TKey, TValue> _producer;

        public ProducerTemplate(IProducer<TKey, TValue> producer)
        {
            _producer = producer;
        }

        public async Task SendAsync(string topic, TKey key, TValue value)
        {
            await _producer.ProduceAsync(topic, new Message<TKey, TValue> { Key = key, Value = value });
        }
    }
}