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
        public VerifiedParkingRate SourceRate { get; set; }

        public ParkingRateInstance(DateTime start, DateTime end, VerifiedParkingRate sourceRate)
        {
            validateInputs(start, end, sourceRate);

            this.Start = start;
            this.End = end;
            this.Price = new Price(sourceRate.Price);
            this.SourceRate = sourceRate;
        }

        public static ParkingRateInstance GetRateInstance(VerifiedParkingRate rate, DateTime startDate)
        {
            if (rate == null) throw new ArgumentNullException();
            if (startDate == null) throw new ArgumentNullException();

            ParkingRateInstance rateInstance = null;

            var localDayOfWeek = TimeZoneInfo.ConvertTimeFromUtc(startDate, rate.TzInfo).ToString("ddd").ToLower();

            if (rate.DayOfWeek == localDayOfWeek)
            {
                // create a date that represents midnight in the given timezone
                // this is used as a "base" to calculate time slot pricing
                var utcMidnight = new DateTime(startDate.Year, startDate.Month, startDate.Day, 00, 00, 00, DateTimeKind.Utc);
                var localMidnight = TimeZoneInfo.ConvertTimeFromUtc(utcMidnight, rate.TzInfo);
                var rateInstanceStart = localMidnight.AddHours(rate.StartHour).AddMinutes(rate.StartMinute);
                var rateInstanceEnd = localMidnight.AddHours(rate.EndHour).AddMinutes(rate.EndMinute);
                rateInstance = new ParkingRateInstance(rateInstanceStart, rateInstanceEnd, rate);
            }
            else
            {
                // irrelevant rate; does not apply to this day of week
            }



            return rateInstance;
        }

        private static void validateInputs(DateTimeOffset start, DateTimeOffset end, VerifiedParkingRate rate)
        {
            if (start == null) throw new ArgumentNullException();
            if (end == null) throw new ArgumentNullException();
            if (rate == null) throw new ArgumentNullException();
            if (rate.Price <= 0) throw new ArgumentOutOfRangeException();
            if (start >= end) throw new ArgumentOutOfRangeException();
            if ((end - start).Days > 1) throw new ArgumentOutOfRangeException();
        }

    }
}
