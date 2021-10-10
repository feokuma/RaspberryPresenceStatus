using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RaspberryPresenceStatus.Models;
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

        [HttpPut]
        [Route("/drawbytes")]
        public ActionResult Put(ImageBytesPayload image)
        {
            if (image.ImageBytes is not null)
            {
                string colorsString = image.ImageBytes.Replace(" ", "");
                byte[] imageBytes = Convert.FromHexString(colorsString);
                DisplayService.DrawBytes(imageBytes);
                return new NoContentResult();
            }
            return new BadRequestResult();
        }
    }
}