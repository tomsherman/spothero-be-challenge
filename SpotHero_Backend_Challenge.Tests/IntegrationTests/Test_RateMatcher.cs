using System;
using Xunit;
using FluentAssertions;

namespace SpotHero_Backend_Challenge.Tests.UnitTests
{
    public class Test_RateMatcher
    {

        [Fact]
        public void ValidPrice()
        {
            Retriever.seedRates(); // idempotent

            //{
            //  "days": "mon,tues,thurs",
            //	"times": "0900-2100",
            //	"tz": "America/Chicago",
            //	"price": 1500
            //}
            var price = RateMatcher.getPrice(tuesday10AM, tuesday10AM.AddHours(1));
            price.Should().NotBeNull();
        }

        [Fact]
        public void NoParkingRateAvailable()
        {
            Retriever.seedRates(); // idempotent

            //{
            //  "days": "mon,tues,thurs",
            //	"times": "0900-2100",
            //	"tz": "America/Chicago",
            //	"price": 1500
            //}
            var price = RateMatcher.getPrice(tuesday10AM.AddHours(-2), tuesday10AM);
            price.Should().BeNull();
        }

        [Fact]
        public void InvalidTimeFrame_MoreThan24Hours()
        {
            Retriever.seedRates(); // idempotent

            //{
            //  "days": "mon,tues,thurs",
            //	"times": "0900-2100",
            //	"tz": "America/Chicago",
            //	"price": 1500
            //}
            Action retrieval = () => RateMatcher.getPrice(tuesday10AM.AddDays(-1), tuesday10AM.AddMinutes(1));
            retrieval.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void InvalidTimeFrame_NoActualTimeRange()
        {
            Retriever.seedRates(); // idempotent

            //{
            //  "days": "mon,tues,thurs",
            //	"times": "0900-2100",
            //	"tz": "America/Chicago",
            //	"price": 1500
            //}
            Action retrieval = () => RateMatcher.getPrice(tuesday10AM, tuesday10AM);
            retrieval.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void InvalidTimeFrame_IllogicalTimeFrame()
        {
            Retriever.seedRates(); // idempotent

            //{
            //  "days": "mon,tues,thurs",
            //	"times": "0900-2100",
            //	"tz": "America/Chicago",
            //	"price": 1500
            //}
            Action retrieval = () => RateMatcher.getPrice(tuesday10AM.AddMinutes(1), tuesday10AM.AddMinutes(-1));
            retrieval.Should().Throw<ArgumentOutOfRangeException>();
        }

        private DateTime tuesday10AM { 
            get
            {
                var tzInfo = TimeZoneConverter.TZConvert.GetTimeZoneInfo("America/Chicago");
                var utcDate = new DateTime(2021, 10, 26, 10, 00, 00, DateTimeKind.Utc);
                var date = TimeZoneInfo.ConvertTime(utcDate, tzInfo);
                return date;
            }
        }

    }
}
