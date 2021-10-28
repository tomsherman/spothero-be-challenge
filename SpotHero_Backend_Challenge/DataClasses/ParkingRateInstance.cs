using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace SpotHero_Backend_Challenge
{
    /// <summary>
    /// Represents an instance of an offered rate that encompasses <= 24 hours of parking time
    /// </summary>
    public class ParkingRateInstance
    {
        public Price price { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }

        public ParkingRateInstance(DateTime start, DateTime end, int price)
        {
            validateInputs(start, end, price);

            this.start = start;
            this.end = end;
            this.price = new Price(price);
        }

        public static ParkingRateInstance getRateInstance(VerifiedParkingRate rate, DateTime date)
        {
            if (rate == null) throw new ArgumentNullException();
            if (date == null) throw new ArgumentNullException();

            ParkingRateInstance rateInstance = null;

            var abbrevDay = date.ToString("ddd").ToLower(); // e.g. "tue"

            var dateBase = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, DateTimeKind.Utc);
            var midnight = TimeZoneInfo.ConvertTime(dateBase, rate.tzInfo);

            // compare day of input date to day in rate definition
            if (abbrevDay == rate.dayOfWeek)
            {
                var rateInstanceStart = midnight.AddHours(rate.startHour).AddMinutes(rate.startMinute);
                var rateInstanceEnd = midnight.AddHours(rate.endHour).AddMinutes(rate.endMinute);
                rateInstance = new ParkingRateInstance(rateInstanceStart, rateInstanceEnd, rate.price);
            }

            return rateInstance;
        }

        private static void validateInputs(DateTime start, DateTime end, int price)
        {
            if (start == null) throw new ArgumentNullException();
            if (end == null) throw new ArgumentNullException();
            if (price <= 0) throw new ArgumentOutOfRangeException();
            if (start >= end) throw new ArgumentOutOfRangeException();
            if ((end - start).Days > 1) throw new ArgumentOutOfRangeException();
        }

    }
}
