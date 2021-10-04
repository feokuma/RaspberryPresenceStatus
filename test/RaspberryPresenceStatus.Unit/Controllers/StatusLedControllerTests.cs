using System.Threading.Tasks;
using Autofac.Extras.FakeItEasy;
using FakeItEasy;
using NUnit.Framework;
using RaspberryPresenceStatus.Controllers;
using RaspberryPresenceStatus.Models.Enuns;
using RaspberryPresenceStatus.Services;

namespace RaspberryPresenceStatus.Unit.Controllers
{
    public class StatusLedControllerTests
    {
        [Test]
        public async Task PutShouldCallDrawAvaliableOfDisplayService()
        {
            using var fake = new AutoFake();
            var status = PresenceStatusEnum.Avaliable;
            var controller = fake.Resolve<StatusLedController>();

            await controller.Put(status);

            A.CallTo(() => fake.Resolve<IDisplayService>().DrawStatus(status))
                .MustHaveHappenedOnceExactly();
        }
    }
}