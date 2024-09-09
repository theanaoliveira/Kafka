using Confluent.Kafka;
using Kafka.Infrastructure.Interfaces;

namespace Kafka.Infrastructure.Consumers
{
    public class ConsumerTemplate<TKey, TValue> : IConsumerTemplate<TKey, TValue>
    {
        private readonly IConsumer<TKey, TValue> consumer;

        public ConsumerTemplate(IConsumer<TKey, TValue> consumer)
        {
            this.consumer = consumer;
        }

        public void ConsumeMessages(CancellationToken cancellationToken)
        {
            consumer.Subscribe("str-topic");

            //var consumeResult = consumer.();

            //while (!cancellationToken.IsCancellationRequested)
            //{

            //}
        }
    }
}
