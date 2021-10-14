using System;
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
        [TestCase(PresenceStatusEnum.Avaliable)]
        [TestCase(PresenceStatusEnum.Busy)]
        [TestCase(PresenceStatusEnum.DoNotDisturb)]
        [TestCase(PresenceStatusEnum.Away)]
        [TestCase(PresenceStatusEnum.Offline)]
        public void DrawPresenceStatusShouldGetStatusImageFromEnumWithStatusReceived(PresenceStatusEnum statusEnum)
        {
            using var fake = new AutoFake();
            var controller = fake.Resolve<StatusLedController>();

            controller.DrawPresenceStatus(statusEnum);

            A.CallTo(() => fake.Resolve<IMicrosoftTeamsStatusImages>().StatusImageFromEnum(statusEnum))
                .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void DrawPresenceStatusShouldCallDrawBytesFromDisplayServiceWithImageBytesFromIMicrosoftTeamsStatusImages()
        {
            using var fake = new AutoFake();
            var controller = fake.Resolve<StatusLedController>();
            var fakeImageBytes = new byte[] { 0x00, 0x01 };
            A.CallTo(() => fake.Resolve<IMicrosoftTeamsStatusImages>().StatusImageFromEnum(A<PresenceStatusEnum>._))
                .Returns(fakeImageBytes);

            controller.DrawPresenceStatus(GetRandomPresenceEnumStatus());

            A.CallTo(() => fake.Resolve<IDisplayService>().DrawBytes(fakeImageBytes))
                .MustHaveHappenedOnceExactly();
        }



        private static PresenceStatusEnum GetRandomPresenceEnumStatus()
        {
            var random = new Random();
            var values = typeof(PresenceStatusEnum).GetEnumValues();

            return (PresenceStatusEnum)random.Next(values.Length);
        }
    }
}