using System;
using Xunit;
using FluentAssertions;

namespace SpotHero_Backend_Challenge.Tests.UnitTests
{
    public class Test_ParkingRateInstance
    {

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void InvalidPrice(int price)
        {
            Action instantiation = () => new ParkingRateInstance(DateTime.Now.AddHours(-1), DateTime.Now, price);
            instantiation.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(10)]
        public void ValidPrice(int price)
        {
            Action instantiation = () => new ParkingRateInstance(DateTime.Now.AddHours(-1), DateTime.Now, price);
            instantiation.Should().NotBeNull();
        }

        [Fact]
        public void InvalidDateRange_Same()
        {
            var now = DateTime.Now;
            Action instantiation = () => new ParkingRateInstance(now, now, 10);
            instantiation.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void InvalidDateRange_StartEndInvalid()
        {
            Action instantiation = () => new ParkingRateInstance(DateTime.Now.AddHours(1), DateTime.Now, 10);
            instantiation.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void GetRateInstance_NullInput()
        {
            Action factory = () =>
            {
                //var verifiedRate = new VerifiedParkingRate("mon", 5, 0, 10, 0, TimeZoneInfo.Local, 1000);
                ParkingRateInstance.getRateInstance(null, DateTime.Now);
            };
            factory.Should().Throw<ArgumentNullException>();
        }

    }
}
