using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace SpotHero_Backend_Challenge.Tests.IntegrationTests
{
    public class Test_Retriever
    {
        [Fact]
        public void RatesPresentInDb()
        {
            var rates = Retriever.GetRates();
            rates.Should().NotBeNull();
        }

        [Fact]
        public void UpdateRatesInDb()
        {
            Action update = () => Retriever.UpdateRates(getRateCollection());
            update.Should().NotThrow();
        }

        [Fact]
        public void SeedRates()
        {
            Action seed = () => Retriever.SeedRates();
            seed.Should().NotThrow();
        }

        private static ParkingRateCollection getRateCollection()
        {
            var inputs = new List<UnverifiedParkingRateInput>
            {
                new UnverifiedParkingRateInput() { days = "mon,tue", price = 1000, times = "0900-1500", tz = "America/Chicago" },
                new UnverifiedParkingRateInput() { days = "mon,tue", price = 2000, times = "1500-1800", tz = "America/Chicago" },
                new UnverifiedParkingRateInput() { days = "wed,sat", price = 3000, times = "1000-1800", tz = "America/Chicago" }
            };
            return new ParkingRateCollection(inputs);
        }
    }
}
