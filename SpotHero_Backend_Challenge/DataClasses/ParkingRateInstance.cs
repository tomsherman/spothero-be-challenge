using System;

namespace SpotHero_Backend_Challenge
{
    /// <summary>
    /// Represents an instance of an offered rate that encompasses less than or equal to 24 hours of parking time
    /// </summary>
    public class ParkingRateInstance
    {
        public Price Price { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public VerifiedParkingRate SourceRate { get; set; }

        public ParkingRateInstance(DateTimeOffset start, DateTimeOffset end, VerifiedParkingRate sourceRate)
        {
            validateInputs(start.LocalDateTime, end.LocalDateTime, sourceRate);

            this.Start = start.LocalDateTime;
            this.End = end.LocalDateTime;
            this.Price = new Price(sourceRate.Price);
            this.SourceRate = sourceRate;
        }

        public static ParkingRateInstance GetRateInstance(VerifiedParkingRate rate, DateTimeOffset startDate)
        {
            if (rate == null) throw new ArgumentNullException();
            if (startDate == null) throw new ArgumentNullException();

            ParkingRateInstance rateInstance = null;

            var startDateUtc = startDate.UtcDateTime;

            var localDayOfWeek = TimeZoneInfo.ConvertTimeFromUtc(startDateUtc, rate.TzInfo).ToString("ddd").ToLower();

            // todo off by one day!

            if (rate.DayOfWeek == localDayOfWeek)
            {
                var midnightInInputTz = new DateTimeOffset(startDateUtc.Year,
                    startDateUtc.Month,
                    startDateUtc.Day, 
                    00, 
                    00, 
                    00, 
                    rate.TzInfo.GetUtcOffset(startDate));

                //// adjust for GMT
                //if (rate.TzInfo.GetUtcOffset(startDate).TotalSeconds > 0)
                //{
                //    midnightInInputTz.AddDays(1);
                //}

                //var midnightInInputTz = startDate
                //    .UtcDateTime
                //    .AddHours(-1 * startDate.Hour)
                //    .AddMinutes(-1 * startDate.Minute)
                //    .AddSeconds(-1 * startDate.Second)
                //    .AddMilliseconds(-1 * startDate.Millisecond)
                //    .UtcDateTime
                //    .Subtract(rate.TzInfo.GetUtcOffset(startDate));

                // represent the rate instances dates in the timezone of the offered rate
                var rateInstanceStart = midnightInInputTz
                    .AddHours(rate.StartHour)
                    .AddMinutes(rate.StartMinute);
                var rateInstanceEnd = midnightInInputTz
                    .AddHours(rate.EndHour)
                    .AddMinutes(rate.EndMinute);
                rateInstance = new ParkingRateInstance(rateInstanceStart, rateInstanceEnd, rate);
            }
            else
            {
                // irrelevant rate; does not apply to this day of week
            }

            return rateInstance;
        }

        private static void validateInputs(DateTime start, DateTime end, VerifiedParkingRate rate)
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
