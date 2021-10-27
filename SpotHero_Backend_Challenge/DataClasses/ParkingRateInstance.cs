using System;
using System.Collections.Generic;

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
            if (start == null) throw new ArgumentNullException();
            if (end == null) throw new ArgumentNullException();
            if (price <= 0) throw new ArgumentOutOfRangeException();
            if (start <= end) throw new ArgumentOutOfRangeException();
            if ((end - start).Days > 1) throw new ArgumentOutOfRangeException();

            this.start = start;
            this.end = end;
            this.price = new Price { price = price };
        }
    }
}
