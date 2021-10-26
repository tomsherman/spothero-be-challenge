using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SpotHero_Backend_Challenge
{
    internal class RateInstance
    {
        public Price price { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }
}
