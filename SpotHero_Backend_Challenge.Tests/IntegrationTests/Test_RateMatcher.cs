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
        public void ValidPriceMaxDuration_Cairo()
        {
            Retriever.SeedRates(); // idempotent

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
                var utcDate = new DateTime(2021, 10, 26, 10, 00, 00, DateTimeKind.Unspecified);
                var date = TimeZoneInfo.ConvertTime(utcDate, tzInfo);
                return date;
            }
        }

        //private DateTime tuesday10AM_Cairo
        //{
        //    get
        //    {
        //        var tzInfo = TimeZoneConverter.TZConvert.GetTimeZoneInfo("Africa/Cairo");
        //        var utcDate = new DateTime(2021, 10, 26, 10, 00, 00, DateTimeKind.Unspecified);
        //        var date = TimeZoneInfo.ConvertTime(utcDate, tzInfo);
        //        return date;
        //    }
        //}

        private DateTime wednesday6AM_Cairo
        {
            get
            {
                var tzInfo = TimeZoneConverter.TZConvert.GetTimeZoneInfo("Africa/Cairo");
                var utcDate = new DateTime(2021, 10, 27, 0, 00, 00, DateTimeKind.Utc);
                var localMidnight = utcDate + tzInfo.GetUtcOffset(utcDate);
                var date = localMidnight.AddHours(6);
                return date;
            }
        }


        /*
         * 
         * 
            // create a date that represents midnight in the given timezone
            // this is used as a "base" to calculate time slot pricing
            var dateBase = new DateTimeOffset(startDate.Year, startDate.Month, startDate.Day, 00, 00, 00, startDate.Offset);



            // todo test with rates in Cairo time but input in Chicago time
            DateTimeOffset midnight = TimeZoneInfo.ConvertTime(dateBase, rate.TzInfo);
         * 
         * 
         * */

    }
}
