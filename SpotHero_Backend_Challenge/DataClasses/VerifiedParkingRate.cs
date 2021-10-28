using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SpotHero_Backend_Challenge
{
    // https://github.com/spothero/be-code-challenge#sample-json-for-testing
    //{
    //    "days": "mon,tues,thurs",
    //    "times": "0900-2100",
    //    "tz": "America/Chicago",
    //    "price": 1500
    //}
    public class VerifiedParkingRate
    {
        public string dayOfWeek { get; set; }
        public int startHour { get; set; }
        public int startMinute { get; set; }
        public int endHour { get; set; }
        public int endMinute { get; set; }
        public TimeZoneInfo tzInfo { get; set; }
        public int price { get; set; }

        private static Dictionary<string, string> nonStandardDayOfWeekDictionary = new Dictionary<string, string>() { 
            { "mon", "mon" },
            { "tue", "tue" },
            { "tues", "tue" },
            { "wed", "wed" },
            { "thu", "thu" },
            { "thurs", "thu" },
            { "fri", "fri" },
            { "sat", "sat" },
            { "sun", "sun" }
        };

        public VerifiedParkingRate(string dayOfWeek, int startHour, int startMinute, int endHour, int endMinute, TimeZoneInfo tzInfo, int price)
        {
            if (!nonStandardDayOfWeekDictionary.ContainsKey(dayOfWeek)) 
                throw new ArgumentException($"invalid day specified: {dayOfWeek}");

            if (startHour < 0 || startHour > 23) throw new ArgumentException();
            if (endHour < 0 || endHour > 23) throw new ArgumentException();
            if (startMinute < 0 || startMinute > 59) throw new ArgumentException();
            if (endMinute < 0 || endMinute > 59) throw new ArgumentException();
            if (tzInfo == null) throw new ArgumentException();
            if (price <= 0) throw new ArgumentException();
            if (startHour > endHour) throw new ArgumentException();
            if (startHour == endHour && startMinute >= endMinute) throw new ArgumentException();

            this.dayOfWeek = dayOfWeek;
            this.startHour = startHour;
            this.startMinute = startMinute;
            this.endHour = endHour;
            this.endMinute = endMinute;
            this.tzInfo = tzInfo;
            this.price = price;
        }

        public static List<VerifiedParkingRate> getVerifiedRates(UnverifiedParkingRateInput rate)
        {
            var verifiedRates = new List<VerifiedParkingRate>();

            // check for the simple stuff
            // this data is unverified and coming from the client (or perhaps from a database,
            // where the data could also be bad)
            validateRate(rate);

            // expecting times of format: 0100-2300
            if (!new Regex(@"^\d{4}\-\d{4}$").IsMatch(rate.times)) throw new ArgumentException();

            // format: "0900-2100",
            var startHHMM = rate.times.Split('-')[0];
            var startHour = int.Parse(startHHMM.Substring(0, 2));
            var startMinute = int.Parse(startHHMM.Substring(2));

            var endHHMM = rate.times.Split('-')[1];
            var endHour = int.Parse(endHHMM.Substring(0, 2));
            var endMinute = int.Parse(endHHMM.Substring(2));

            // IANA timezone format
            var tzInfo = TimeZoneConverter.TZConvert.GetTimeZoneInfo(rate.tz);

            var days = rate.days.Split(",", StringSplitOptions.RemoveEmptyEntries);

            foreach (string day in days)
            {
                verifiedRates.Add(new VerifiedParkingRate(
                    nonStandardDayOfWeekDictionary[day], 
                    startHour, 
                    startMinute, 
                    endHour,
                    endMinute, 
                    tzInfo, 
                    rate.price));
            }

            return verifiedRates;
        }

        private static void validateRate(UnverifiedParkingRateInput rate)
        {
            if (rate == null) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(rate.times)) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(rate.days)) throw new ArgumentNullException();
            if (rate.price <= 0) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(rate.tz)) throw new ArgumentNullException();
        }
    }
}
