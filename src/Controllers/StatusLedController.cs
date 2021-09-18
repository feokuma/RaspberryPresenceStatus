using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RaspberryPresenceStatus.Services;

namespace RaspberryPresenceStatus.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class StatusLedController
    {
        public ILogger<StatusLedController> Logger { get; private set; }
        public IDisplayService DisplayService { get; }

        public StatusLedController(ILogger<StatusLedController> logger, IDisplayService displayService)
        {
            Logger = logger;
            DisplayService = displayService;
        }

        [HttpPut]
        public async Task<ActionResult> Put(byte[] statusLed)
        {
            DisplayService.DrawBytes(statusLed);
            return await Task.FromResult(new NoContentResult());
        }
    }
}