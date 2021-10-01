using System.Threading.Tasks;
using Autofac.Extras.FakeItEasy;
using FakeItEasy;
using NUnit.Framework;
using RaspberryPresenceStatus.Controllers;
using RaspberryPresenceStatus.Services;

namespace RaspberryPresenceStatus.Unit.Controllers
{
    public class StatusLedControllerTests
    {
        [Test]
        public async Task PutShouldCallDrawAvaliableOfDisplayService()
        {
            using var fake = new AutoFake();
            var controller = fake.Resolve<StatusLedController>();

            await controller.Put();

            A.CallTo(() => fake.Resolve<IDisplayService>().DrawAvaliable())
                .MustHaveHappenedOnceExactly();
        }
    }
}