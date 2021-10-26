using System;
using System.Collections.Generic;

namespace SpotHero_Backend_Challenge
{
    public class RateMatcher
    {
        public static Price getPrice(DateTime start, DateTime end)
        {
            var timeSpan = end - start;
            if (timeSpan.TotalDays > 0 && timeSpan.TotalDays <= 1)
            {
                // use requested start date to generate a slate of rate instances
                // near the requested timeframe
                var rateInstances = getRateInstances(start); // use start date to generate slate of 
                foreach (RateInstance rateInstance in rateInstances)
                {
                    if (rateInstance.start <= start && rateInstance.end >= end)
                    {
                        // guaranteed no overlap; exit early
                        return rateInstance.price;
                    }
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("Invalid dates");
            }

            // no match
            return null;
        }

        private static List<RateInstance> getRateInstances(DateTime date)
        {
            var rates = Retriever.getRates(); // todo call MongstartoDB

            var allRateInstances = new List<RateInstance>();
            foreach (Rate rate in rates.rates)
            {
                allRateInstances.AddRange(generateRateInstance(rate, date.AddDays(-1))); // preceding day
                allRateInstances.AddRange(generateRateInstance(rate, date));
                allRateInstances.AddRange(generateRateInstance(rate, date.AddDays(+1))); // next day
            }

            return allRateInstances;
        }

        private static List<RateInstance> generateRateInstance(Rate rate, DateTime date)
        {
            var rateInstances = new List<RateInstance>();

            // format: "0900-2100",
            var startHHMM = rate.times.Split('-')[0];
            var endHHMM = rate.times.Split('-')[1];
            var startHour = int.Parse(startHHMM.Substring(0, 2));
            var startMinute = int.Parse(startHHMM.Substring(2));

            var endHour = int.Parse(endHHMM.Substring(0, 2));
            var endMinute = int.Parse(endHHMM.Substring(2));

            var tzInfo = TimeZoneConverter.TZConvert.GetTimeZoneInfo(rate.tz);

            var dateBase = new DateTime(date.Year, date.Month, date.Day);
            var midnight = TimeZoneInfo.ConvertTime(dateBase, tzInfo);

            foreach (string day in rate.days.Split(",")) // todo move to friendlier method
            {
                // todo real date
                if (midnight.ToString("ddd").ToLower() == day)
                {
                    var rateInstanceStart = midnight.AddHours(startHour).AddMinutes(startMinute);
                    var rateInstanceEnd= midnight.AddHours(endHour).AddMinutes(endMinute);

                    rateInstances.Add(new RateInstance() { 
                        start = rateInstanceStart,
                        end = rateInstanceEnd,
                        price = new Price { price = rate.price }
                    });
                }
            }

            return rateInstances;
        }
 
    }
}
