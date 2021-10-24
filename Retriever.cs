using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotHero_Backend_Challenge
{
    public class Retriever
    {
        private static List<Rate> fakeData;

        public static List<Rate> getRates()
        {
            return fakeData; // todo
        }

        public static void updateRates(List<Rate> rates)
        {
            fakeData = rates;
        }

        public static Rate getRate(DateTime start, DateTime end)
        {
            return new Rate();
        }

        public static void seedRates()
        {

            // {
            //    "rates": [
            //        {
            //            "days": "mon,tues,thurs",
            //            "times": "0900-2100",
            //            "tz": "America/Chicago",
            //            "price": 1500
            //        },
            //        {
            //            "days": "fri,sat,sun",
            //            "times": "0900-2100",
            //            "tz": "America/Chicago",
            //            "price": 2000
            //        },
            //        {
            //            "days": "wed",
            //            "times": "0600-1800",
            //            "tz": "America/Chicago",
            //            "price": 1750
            //        },
            //        {
            //            "days": "mon,wed,sat",
            //            "times": "0100-0500",
            //            "tz": "America/Chicago",
            //            "price": 1000
            //        },
            //        {
            //            "days": "sun,tues",
            //            "times": "0100-0700",
            //            "tz": "America/Chicago",
            //            "price": 925
            //        }
            //    ]
            //}

            var rate1 = new Rate()
            {
                days = "mon,tues,thurs",
                times = "0900-2100",
                tz = "America/Chicago",
                price = 1500
            };
            var rate2 = new Rate()
            {
                days = "fri,sat,sun",
                times = "0900-2100",
                tz = "America/Chicago",
                price = 2000
            };
            var rate3 = new Rate()
            {
                days = "wed",
                times = "0600-1800",
                tz = "America/Chicago",
                price = 1750
            };
            var rate4 = new Rate()
            {
                days = "mon,wed,sat",
                times = "0100-0500",
                tz = "America/Chicago",
                price = 1000
            };
            var rate5 = new Rate()
            {
                days = "sun,tues",
                times = "0100-0700",
                tz = "America/Chicago",
                price = 925
            };

            fakeData = new List<Rate>() { rate1, rate2, rate3, rate4, rate5 };

        }

    }
}
