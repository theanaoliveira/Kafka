namespace Kafka.Infrastructure.Interfaces
{
    public interface IPublishMessages<TValue>
    {
        Task SendMessage(TValue message);
    }
}
