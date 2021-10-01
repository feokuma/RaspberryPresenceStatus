using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RaspberryPresenceStatus.Models.Enuns;
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
        [Route("/status")]
        public async Task<ActionResult> Put(PresenceStatusEnum presenceStatusEnum)
        {
            DisplayService.DrawStatus(presenceStatusEnum);
            return await Task.FromResult(new NoContentResult());
        }
    }
}