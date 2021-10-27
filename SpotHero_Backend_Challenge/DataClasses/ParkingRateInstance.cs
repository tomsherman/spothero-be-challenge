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

        public static List<ParkingRateInstance> getRateInstances(ParkingRate rate, DateTime date)
        {
            validateRate(rate);
            if (date == null) throw new ArgumentNullException();

            var rateInstances = new List<ParkingRateInstance>();

            // expecting times of format: 0100-2300
            if (!new Regex(@"^\d{4}\-\d{4}$").IsMatch(rate.times)) throw new ArgumentException();
            var startHHMM = rate.times.Split('-')[0];
            var startHour = int.Parse(startHHMM.Substring(0, 2));
            var startMinute = int.Parse(startHHMM.Substring(2));

            var endHHMM = rate.times.Split('-')[1];
            var endHour = int.Parse(endHHMM.Substring(0, 2));
            var endMinute = int.Parse(endHHMM.Substring(2));

            // IANA timezone format
            var tzInfo = TimeZoneConverter.TZConvert.GetTimeZoneInfo(rate.tz);
            if (tzInfo == null) throw new ArgumentException();

            var dateBase = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, DateTimeKind.Utc);
            var midnight = TimeZoneInfo.ConvertTime(dateBase, tzInfo);

            // format: mon,tues,wed,thurs (non-standard abbreviations)
            var validDays = new HashSet<string>() { "mon", "tues", "wed", "thurs", "fri", "sat", "sun" };
            var days = rate.days.Split(",", StringSplitOptions.RemoveEmptyEntries);
            foreach (string day in days)
            {
                if (!validDays.Contains(day)) throw new ArgumentOutOfRangeException($"invalid day specified: {day}");
            }

            foreach (string day in days)
            {
                var abbrevDay = getNonStandardDayAbbreviation(midnight.ToString("ddd").ToLower());

                // compare day of input date to day in rate definition
                if (abbrevDay == day)
                {
                    var rateInstanceStart = midnight.AddHours(startHour).AddMinutes(startMinute);
                    var rateInstanceEnd = midnight.AddHours(endHour).AddMinutes(endMinute);

                    rateInstances.Add(new ParkingRateInstance(rateInstanceStart, rateInstanceEnd, rate.price));
                }
            }

            return rateInstances;
        }

        private static string getNonStandardDayAbbreviation(string dayAbbrev3Letter)
        {
            var abbrevDay = dayAbbrev3Letter;
            
            switch (abbrevDay)
            {
                case "tue":
                    abbrevDay = "tues";
                    break;
                case "thu":
                    abbrevDay = "thurs";
                    break;
                default:
                    // do nothing; normal 3-letter abbreviation
                    break;
            }

            return abbrevDay;
        }

        private static void validateInputs(DateTime start, DateTime end, int price)
        {
            if (start == null) throw new ArgumentNullException();
            if (end == null) throw new ArgumentNullException();
            if (price <= 0) throw new ArgumentOutOfRangeException();
            if (start >= end) throw new ArgumentOutOfRangeException();
            if ((end - start).Days > 1) throw new ArgumentOutOfRangeException();
        }

        private static void validateRate(ParkingRate rate)
        {
            if (rate == null) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(rate.times)) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(rate.days)) throw new ArgumentNullException();
            if (rate.price <= 0) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(rate.tz)) throw new ArgumentNullException();
        }
    }
}
