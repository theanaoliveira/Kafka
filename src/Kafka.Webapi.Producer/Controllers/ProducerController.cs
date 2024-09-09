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
        [ProducesResponseType(201)]
        public async Task<IActionResult> SendMessage([FromBody] string message)
        {
            await publishMessages.SendMessage(message);

            return Created(new Uri($"{Request.Path}", UriKind.Relative), "Mensagem publicada");
        }
    }
}