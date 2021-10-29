using System;
using System.Collections.Generic;

namespace SpotHero_Backend_Challenge
{
    public class RateMatcher
    {
        /// <summary>
        /// Given an input date range, retrieves rates from the database and looks for matches.
        /// </summary>
        /// <param name="start">requested start date for parking</param>
        /// <param name="end">requested end date for parking</param>
        public static Price GetPrice(DateTime start, DateTime end)
        {
            start = DateTime.SpecifyKind(start, DateTimeKind.Utc);
            end = DateTime.SpecifyKind(end, DateTimeKind.Utc);

            var timeSpan = end - start;
            if (timeSpan.TotalDays <= 0 || timeSpan.TotalDays > 1)
                throw new ArgumentOutOfRangeException("Invalid date range. Specify a range of 24 hours or less.");

            // use requested start date to generate a slate of rate instances
            // near the requested timeframe
            var rateInstances = getRateInstances(start);

            // determine if any rate instance fully contains the time span requested
            foreach (ParkingRateInstance rateInstance in rateInstances)
            {
                if (start >= rateInstance.Start && end <= rateInstance.End)
                {
                    // guaranteed no overlap; exit early
                    return rateInstance.Price;
                }
            }

            // valid input but no matching rate instance
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate">requested start date for parking</param>
        /// <returns></returns>
        private static List<ParkingRateInstance> getRateInstances(DateTime startDate)
        {
            var rateInstances = new List<ParkingRateInstance>();
            var rateCollection = Retriever.GetRates();

            foreach (UnverifiedParkingRateInput rateInput in rateCollection.rates)
            {
                // validates all input rates
                // errors here will throw an exception
                // depending on requirements, this error handling behavior could be adjusted,
                // i.e. if we want to be fault tolerant of some-but-not-all bad data
                var verifiedRates = VerifiedParkingRate.GetVerifiedRates(rateInput);

                foreach(VerifiedParkingRate verifiedRate in verifiedRates)
                {
                    var instance = ParkingRateInstance.GetRateInstance(verifiedRate, startDate);
                    if (instance != null) rateInstances.Add(instance);
                }
            }

            return rateInstances;
        }
 
    }
}
