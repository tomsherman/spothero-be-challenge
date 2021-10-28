using System;
using System.Collections.Generic;

namespace SpotHero_Backend_Challenge
{
    public class RateMatcher
    {
        public static Price GetPrice(DateTime start, DateTime end)
        {
            var timeSpan = end - start;
            if (timeSpan.TotalDays <= 0 || timeSpan.TotalDays > 1)
                throw new ArgumentOutOfRangeException("Invalid date range. Specify a range of 24 hours or less.");

            // use requested start date to generate a slate of rate instances
            // near the requested timeframe
            var rateInstances = getRateInstances(start);
            foreach (ParkingRateInstance rateInstance in rateInstances)
            {
                if (rateInstance.Start <= start && rateInstance.End >= end)
                {
                    // guaranteed no overlap; exit early
                    return rateInstance.Price;
                }
            }

            // valid input but no matching rate instance
            return null;
        }

        private static List<ParkingRateInstance> getRateInstances(DateTime date)
        {
            var rateInstances = new List<ParkingRateInstance>();
            var rateCollection = Retriever.GetRates();

            foreach (UnverifiedParkingRateInput rateInput in rateCollection.rates)
            {
                var verifiedRates = VerifiedParkingRate.GetVerifiedRates(rateInput);
                foreach(VerifiedParkingRate verifiedRate in verifiedRates)
                {
                    var instance = ParkingRateInstance.GetRateInstance(verifiedRate, date);
                    if (instance != null) rateInstances.Add(instance);
                }
            }

            return rateInstances;
        }
 
    }
}
