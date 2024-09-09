using Kafka.Infrastructure.Interfaces;

namespace Kafka.Infrastructure.Services
{
    public class PublishMessages<TValue> : IPublishMessages<TValue>
    {
        private readonly IProducerTemplate<string, TValue> _template;

        public PublishMessages(IProducerTemplate<string, TValue> template)
        {
            _template = template;
        }

        public async Task SendMessage(TValue message)
        {
            await _template.SendAsync("str-topic", "", message)
                .ContinueWith(task =>
                {
                    if (task.IsCompletedSuccessfully)
                        Console.WriteLine("Send message with success");
                    else
                        Console.WriteLine("Occurred an error");
                });
        }
    }
}
