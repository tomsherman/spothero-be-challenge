using System;

namespace SpotHero_Backend_Challenge
{
    /// <summary>
    /// Represents an instance of an offered rate that encompasses less than or equal to 24 hours of parking time
    /// </summary>
    /// <remarks>
    /// As this is an offering in a specific time range, timezone is irrelevant here. 
    /// </remarks>
    public class ParkingRateInstance
    {
        public Price Price { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public ParkingRateInstance(DateTime start, DateTime end, int price)
        {
            validateInputs(start, end, price);

            this.Start = start;
            this.End = end;
            this.Price = new Price(price);
        }

        public static ParkingRateInstance GetRateInstance(VerifiedParkingRate rate, DateTime date)
        {
            if (rate == null) throw new ArgumentNullException();
            if (date == null) throw new ArgumentNullException();

            ParkingRateInstance rateInstance = null;

            var abbrevDay = date.ToString("ddd").ToLower(); // e.g. "tue"

            // create a date that represents midnight in the given timezone
            // this is used as a "base" to calculate time slot pricing
            var dateBase = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, DateTimeKind.Utc);
            var midnight = TimeZoneInfo.ConvertTime(dateBase, rate.TzInfo);

            // compare day of input date to day in rate definition
            // uses specific timezone
            if (abbrevDay == rate.DayOfWeek)
            {
                var rateInstanceStart = midnight.AddHours(rate.StartHour).AddMinutes(rate.StartMinute);
                var rateInstanceEnd = midnight.AddHours(rate.EndHour).AddMinutes(rate.EndMinute);
                rateInstance = new ParkingRateInstance(rateInstanceStart, rateInstanceEnd, rate.Price);
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
