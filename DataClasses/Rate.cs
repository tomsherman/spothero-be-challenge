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
    public class Rate
    {
        public string days;
        public string times;
        public string tz;
        public int price;
    }
}
