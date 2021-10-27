using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpotHero_Backend_Challenge
{
    public class ParkingRateCollection
    {
        [Required]
        public List<ParkingRate> rates { get; set; } = new List<ParkingRate>();

        public ParkingRateCollection(List<ParkingRate> rates)
        {
            if (rates == null) throw new ArgumentNullException();
            if (rates.Count == 0) throw new ArgumentException();
            this.rates = rates;
        }
    }
}
