using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace SpotHero_Backend_Challenge.Tests.UnitTests
{
    public class Test_ParkingRateCollection
    {

        [Fact]
        public void NullRates()
        {
            Action instantiation = () => new ParkingRateCollection(null);
            instantiation.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void EmptyRateList()
        {
            Action instantiation = () => new ParkingRateCollection(new List<UnverifiedParkingRateInput>() { });
            instantiation.Should().Throw<ArgumentException>();
        }


    }
}
