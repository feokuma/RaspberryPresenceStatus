using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using RaspberryPresenceStatus.Models.Enuns;
using RaspberryPresenceStatus.Services;

namespace RaspberryPresenceStatus.Unit
{
    public class DisplayServiceTests
    {
        [Test]
        public void TestName()
        {
            var status = PresenceStatusEnum.Avaliable;
            var displayService = new DisplayService();

            displayService.DrawStatus(status);
        }
    }
}