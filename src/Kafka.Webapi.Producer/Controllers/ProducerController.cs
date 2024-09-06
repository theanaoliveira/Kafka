using Kafka.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kafka.Webapi.Producer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IPublishMessages<string> publishMessages;

        public ProducerController(IPublishMessages<string> publishMessages)
        {
            this.publishMessages = publishMessages;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] string message)
        {
            await publishMessages.SendMessage(message);

            return NoContent();
        }
    }
}