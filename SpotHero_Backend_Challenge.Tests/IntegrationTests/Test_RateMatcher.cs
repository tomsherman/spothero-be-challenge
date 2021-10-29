using System;
using Xunit;
using FluentAssertions;

namespace SpotHero_Backend_Challenge.Tests.IntegrationTests
{
    public class Test_RateMatcher
    {

        [Fact]
        public void ValidPrice_Chicago()
        {
            Retriever.SeedRatesChicago(); // idempotent

            //{
            //  "days": "mon,tues,thurs",
            //	"times": "0900-2100",
            //	"tz": "America/Chicago",
            //	"price": 1500
            //}
            var price = RateMatcher.GetPrice(tuesday10AM_Chicago, tuesday10AM_Chicago.AddHours(1));
            price.Should().NotBeNull();
        }

        [Fact]
        public void ValidPriceMaxDuration_Cairo()
        {
            Retriever.SeedRatesCairo(); // idempotent

            //{
            //    days = "wed",
            //    times = "0600-1800",
            //    tz = "Africa/Cairo",
            //    price = 4444
            //}
            var price = RateMatcher.GetPrice(wednesday6AM_Cairo, wednesday6AM_Cairo.AddHours(12)); // todo full span test case
            price.Should().NotBeNull();
        }

        // todo test with rates in Cairo time but input in Chicago time

        [Fact]
        public void ValidPriceMaxDuration_Chicago()
        {
            Retriever.SeedRatesChicago(); // idempotent

            //{
            //  "days": "mon,tues,thurs",
            //	"times": "0900-2100",
            //	"tz": "America/Chicago",
            //	"price": 1500
            //}
            var price = RateMatcher.GetPrice(tuesday10AM_Chicago.AddHours(-1), tuesday10AM_Chicago.AddHours(11));
            price.Should().NotBeNull();
        }

        [Fact]
        public void NoParkingRateAvailable()
        {
            Retriever.SeedRatesChicago(); // idempotent

            //{
            //  "days": "mon,tues,thurs",
            //	"times": "0900-2100",
            //	"tz": "America/Chicago",
            //	"price": 1500
            //}
            var price = RateMatcher.GetPrice(tuesday10AM_Chicago.AddHours(-2), tuesday10AM_Chicago);
            price.Should().BeNull();
        }

        [Fact]
        public void InvalidTimeFrame_MoreThan24Hours()
        {
            Retriever.SeedRatesChicago(); // idempotent

            //{
            //  "days": "mon,tues,thurs",
            //	"times": "0900-2100",
            //	"tz": "America/Chicago",
            //	"price": 1500
            //}
            Action retrieval = () => RateMatcher.GetPrice(tuesday10AM_Chicago.AddDays(-1), tuesday10AM_Chicago.AddMinutes(1));
            retrieval.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void InvalidTimeFrame_NoActualTimeRange()
        {
            Retriever.SeedRatesChicago(); // idempotent

            //{
            //  "days": "mon,tues,thurs",
            //	"times": "0900-2100",
            //	"tz": "America/Chicago",
            //	"price": 1500
            //}
            Action retrieval = () => RateMatcher.GetPrice(tuesday10AM_Chicago, tuesday10AM_Chicago);
            retrieval.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void InvalidTimeFrame_IllogicalTimeFrame()
        {
            Retriever.SeedRatesChicago(); // idempotent

            //{
            //  "days": "mon,tues,thurs",
            //	"times": "0900-2100",
            //	"tz": "America/Chicago",
            //	"price": 1500
            //}
            Action retrieval = () => RateMatcher.GetPrice(tuesday10AM_Chicago.AddMinutes(1), tuesday10AM_Chicago.AddMinutes(-1));
            retrieval.Should().Throw<ArgumentOutOfRangeException>();
        }

        private DateTime tuesday10AM_Chicago { 
            get
            {
                var localMidnight = getLocalMidnightInUtc(2021, 10, 26, "America/Chicago");
                return localMidnight.AddHours(10);
            }
        }

        private DateTime wednesday6AM_Cairo
        {
            get
            {
                var localMidnight = getLocalMidnightInUtc(2021, 10, 27, "Africa/Cairo");
                return localMidnight.AddHours(6);
            }
        }

        private static DateTime getLocalMidnightInUtc(int year, int month, int day, string ianaTimezone)
        {
            var tzInfo = TimeZoneConverter.TZConvert.GetTimeZoneInfo(ianaTimezone);
            var utcDate = new DateTime(year, month, day, 00, 00, 00, DateTimeKind.Utc);
            var localMidnight = utcDate + tzInfo.GetUtcOffset(utcDate);
            return localMidnight;
        }

    }
}
