using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RaspberryPresenceStatus.Models;

namespace RaspberryPresenceStatus.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class StatusLedController
    {
        public ILogger<StatusLedController> Logger { get; private set; }
        public StatusLedController(ILogger<StatusLedController> logger)
        {
            Logger = logger;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] StatusLed statusLed)
        {
            return await Task.FromResult(new NoContentResult());
        }
    }
}