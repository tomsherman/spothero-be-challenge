using System;
using Xunit;
using FluentAssertions;
using SpotHero_Backend_Challenge;

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
        [InlineData(1)]
        public void ValidPrice(int price)
        {
            Action instantiation = () => new ParkingRateInstance(DateTime.Now.AddHours(-1), DateTime.Now, price);
            instantiation.Should().NotBeNull();
        }

        [Fact]
        public void InvalidDateRange()
        {
            Action instantiation = () => new ParkingRateInstance(DateTime.Now, DateTime.Now, 10);
            instantiation.Should().Throw<ArgumentOutOfRangeException>();
        }

    }
}
