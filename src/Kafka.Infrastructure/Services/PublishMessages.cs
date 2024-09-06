using Kafka.Infrastructure.Interfaces;

namespace Kafka.Infrastructure.Services
{
    public class PublishMessages<TValue> : IPublishMessages<TValue>
    {
        private readonly IKafkaTemplate<string, TValue> _template;

        public PublishMessages(IKafkaTemplate<string, TValue> template)
        {
            _template = template;
        }

        public async Task SendMessage(TValue message)
        {
            await _template.SendAsync("str-topic", "", message);
        }
    }
}
