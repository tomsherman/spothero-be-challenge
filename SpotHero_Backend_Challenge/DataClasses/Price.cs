using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpotHero_Backend_Challenge
{
    /// <summary>
    /// Represents the rate for a specific time slot.
    /// </summary>
    /// <remarks>Could be extended to specify currency</remarks>
    public class Price
    {
        [Required]
        public int price { get; set; }

        public Price(int price)
        {
            if (price < 0) throw new ArgumentOutOfRangeException();
            this.price = price;
        }
    }
}
