namespace Kafka.Infrastructure.Interfaces
{
    public interface IConsumerMessages
    {
        void Consume(CancellationToken cancellationToken);
    }
}
