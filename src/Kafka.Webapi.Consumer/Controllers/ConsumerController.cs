using Kafka.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kafka.Webapi.Consumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private readonly IConsumerTemplate<string, string>  consumerTemplate;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public ConsumerController(IConsumerTemplate<string, string> consumerTemplate)
        {
            this.consumerTemplate = consumerTemplate;
        }

        [HttpGet]
        public IActionResult Consume()
        {
            consumerTemplate.ConsumeMessages(_cancellationTokenSource.Token);

            return Ok();
        }
    }
}
