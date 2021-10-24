using System;

namespace SpotHero_Backend_Challenge
{
    // https://github.com/spothero/be-code-challenge#sample-json-for-testing
    //{
    //    "days": "mon,tues,thurs",
    //    "times": "0900-2100",
    //    "tz": "America/Chicago",
    //    "price": 1500
    //}
    public class Rate
    {
        public string days { get; set; }
        public string times { get; set; }
        public string tz { get; set; }
        public int price { get; set; }
    }
}
