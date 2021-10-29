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
            Action instantiation = () => new VerifiedParkingRate("mon", 6, 0, 18, 0, TimeZoneInfo.Local, price);
            instantiation.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(10)]
        public void ValidPrice(int price)
        {
            var rate = new VerifiedParkingRate("mon", 6, 0, 18, 0, TimeZoneInfo.Local, price);
            Action instantiation = () => new ParkingRateInstance(DateTime.Now.AddHours(-1), DateTime.Now, rate);
            instantiation.Should().NotBeNull();
        }

        [Fact]
        public void InvalidDateRange_Same()
        {
            var now = DateTime.Now;
            var rate = new VerifiedParkingRate("mon", 6, 0, 18, 0, TimeZoneInfo.Local, 1000);
            Action instantiation = () => new ParkingRateInstance(now, now, rate);
            instantiation.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void InvalidDateRange_StartEndInvalid()
        {
            var rate = new VerifiedParkingRate("mon", 6, 0, 18, 0, TimeZoneInfo.Local, 1000);
            Action instantiation = () => new ParkingRateInstance(DateTime.Now.AddHours(1), DateTime.Now, rate);
            instantiation.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void GetRateInstance_NullInput()
        {
            Action factory = () =>
            {
                //var verifiedRate = new VerifiedParkingRate("mon", 5, 0, 10, 0, TimeZoneInfo.Local, 1000);
                ParkingRateInstance.GetRateInstance(null, DateTime.Now);
            };
            factory.Should().Throw<ArgumentNullException>();
        }

    }
}
