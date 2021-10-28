using System;
using Xunit;
using FluentAssertions;

namespace SpotHero_Backend_Challenge.Tests.IntegrationTests
{
    public class Test_RateMatcher
    {

        [Fact]
        public void ValidPrice()
        {
            Retriever.SeedRates(); // idempotent

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
        public void ValidPriceMaxDuration()
        {
            Retriever.SeedRates(); // idempotent

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
            Retriever.SeedRates(); // idempotent

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
            Retriever.SeedRates(); // idempotent

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
            Retriever.SeedRates(); // idempotent

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
            Retriever.SeedRates(); // idempotent

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
                var tzInfo = TimeZoneConverter.TZConvert.GetTimeZoneInfo("America/Chicago");
                var utcDate = new DateTime(2021, 10, 26, 10, 00, 00, DateTimeKind.Utc);
                var date = TimeZoneInfo.ConvertTime(utcDate, tzInfo);
                return date;
            }
        }

    }
}
