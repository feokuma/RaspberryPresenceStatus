using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using RaspberryPresenceStatus.Services;

namespace RaspberryPresenceStatus.Unit
{
    public class DisplayServiceTests
    {
        [Test]
        public void TestName()
        {
            var displayService = new DisplayService();

            displayService.DrawAvaliable();
        }
    }
}