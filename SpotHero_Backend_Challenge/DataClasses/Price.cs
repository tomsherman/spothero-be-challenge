using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpotHero_Backend_Challenge
{
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
