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
        public IMicrosoftTeamsStatusImages MicrosoftTeamsStatusImages { get; }

        public StatusLedController(ILogger<StatusLedController> logger, IDisplayService displayService, IMicrosoftTeamsStatusImages microsoftTeamsStatusImages)
        {
            Logger = logger;
            DisplayService = displayService;
            MicrosoftTeamsStatusImages = microsoftTeamsStatusImages;
        }

        [HttpPut]
        [Route("/status")]
        public ActionResult DrawPresenceStatus(PresenceStatusEnum presenceStatusEnum)
        {
            var imageBytes = MicrosoftTeamsStatusImages.StatusImageFromEnum(presenceStatusEnum);
            DisplayService.DrawBytes(imageBytes);

            return new NoContentResult();
        }

        [HttpPut]
        [Route("/drawbytes")]
        public ActionResult DrawBytes(ImageBytesPayload image)
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