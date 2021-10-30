using System;
using Xunit;
using FluentAssertions;

namespace SpotHero_Backend_Challenge.Tests.IntegrationTests
{
    public class Test_RateMatcher
    {
        [Theory]
        [InlineData("2021-10-27T00:00:00+03:00", "2021-10-27T00:00:00+03:00")] // same start and end
        [InlineData("2021-10-27T00:00:00+03:00", "2021-10-28T00:01:00+03:00")] // more than 24 hours
        [InlineData("2021-10-28T00:00:00+03:00", "2021-10-27T00:00:00+03:00")] // end date precedes start date
        public void InvalidInputTimeRange(string startDateText, string endDateText)
        {
            Action retrieval = () => RateMatcher.GetPrice(DateTime.Parse(startDateText), DateTime.Parse(endDateText));
            retrieval.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData("2021-10-27T06:00:00-05:00", "2021-10-27T18:00:00-05:00")] // Chicago rate, Chicago input, CDT
        [InlineData("2021-10-27T08:00:00-03:00", "2021-10-27T20:00:00-03:00")] // Chicago rate, Pacific input
        [InlineData("2021-10-27T13:00:00+02:00", "2021-10-28T01:00:00+02:00")] // Chicago rate, Cairo input
        [InlineData("2021-11-10T06:00:00-06:00", "2021-11-10T18:00:00-06:00")] // Chicago rate, Chicago input, CST (no daylight savings)
        public void ValidPrice_ChicagoRates(string startDateText, string endDateText)
        {
            Retriever.SeedRatesChicago(); // idempotent
            var price = RateMatcher.GetPrice(DateTime.Parse(startDateText), DateTime.Parse(endDateText));
            price.Should().NotBeNull();
        }

        [Theory]
        [InlineData(1635332400, 1635375600, "America/Chicago")] // Chicago rate, epoch input representing 10/27/2021 at 6 AM CDT through 10/27/2021 at 6 PM CDT
        [InlineData(1635332400 - 2*60*60, 1635375600 - 2 * 60 * 60, "America/Los_Angeles")] // Chicago rate, epoch input representing 10/27/2021 at 6 AM CDT through 10/27/2021 at 6 PM CDT, non-Chicago timezone
        public void ValidPrice_ChicagoRates_EpochInput(int epochSecondsStart, int epochSecondsEnd, string ianaTimezone)
        {
            Retriever.SeedRatesChicago(); // idempotent
            var price = RateMatcher.GetPrice(epochSecondsStart, epochSecondsEnd, ianaTimezone);
            price.Should().NotBeNull();
        }

        [Theory]
        [InlineData(1635332400, 1635375600 + 60, "America/Chicago")] // Chicago rate, epoch input representing 10/27/2021 at 6 AM CDT through 10/27/2021 at 6:01 PM CDT
        [InlineData(1635332400 - (2 * 60 * 60), 1635375600 - (2 * 60 * 60) + 60, "America/Los_Angeles")] // Chicago rate, epoch input representing 10/27/2021 at 6 AM CDT through 10/27/2021 at 6:01 PM CDT, Pacific time (2 hrs back from Chicago)
        public void NoParkingAvailable_ChicagoRates_EpochInput(int epochSecondsStart, int epochSecondsEnd, string ianaTimezone)
        {
            Retriever.SeedRatesChicago(); // idempotent
            var price = RateMatcher.GetPrice(epochSecondsStart, epochSecondsEnd, ianaTimezone);
            price.Should().BeNull();
        }


        [Theory]
        [InlineData("2021-10-27T06:00:00-05:00", "2021-10-27T18:01:00-05:00")] // Chicago rate, Chicago input
        [InlineData("2021-10-27T08:00:00-03:00", "2021-10-27T20:01:00-03:00")] // Chicago rate, Pacific input
        [InlineData("2021-10-27T13:00:00+02:00", "2021-10-28T01:01:00+02:00")] // Chicago rate, Cairo input
        [InlineData("2021-11-10T06:00:00-06:00", "2021-11-10T18:01:00-06:00")] // Chicago rate, Chicago input, CST
        public void NoParkingAvailable_ChicagoRates(string startDateText, string endDateText)
        {
            Retriever.SeedRatesChicago(); // idempotent
            var price = RateMatcher.GetPrice(DateTime.Parse(startDateText), DateTime.Parse(endDateText));
            price.Should().BeNull();
        }

        [Theory]
        [InlineData("2021-10-27T06:00:00+02:00", "2021-10-27T18:00:00+02:00")] // Cairo rate, Cairo input
        [InlineData("2021-10-26T23:00:00-05:00", "2021-10-27T11:00:00-05:00")] // Cairo rate, Chicago input
        public void ValidPrice_CairoRates(string startDateText, string endDateText)
        {
            Retriever.SeedRatesCairo(); // idempotent
            var price = RateMatcher.GetPrice(DateTime.Parse(startDateText), DateTime.Parse(endDateText));
            price.Should().NotBeNull();
        }

        [Theory]
        [InlineData("2021-10-27T06:00:00+02:00", "2021-10-27T18:01:00+02:00")] // Cairo rate, Cairo input
        [InlineData("2021-10-26T23:00:00-05:00", "2021-10-27T11:01:00-05:00")] // Cairo rate, Chicago input
        public void NoParkingAvailable_CairoRates(string startDateText, string endDateText)
        {
            Retriever.SeedRatesCairo(); // idempotent
            var price = RateMatcher.GetPrice(DateTime.Parse(startDateText), DateTime.Parse(endDateText));
            price.Should().BeNull();
        }

    }
}
