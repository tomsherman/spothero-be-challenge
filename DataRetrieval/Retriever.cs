using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace SpotHero_Backend_Challenge
{
    public class Retriever
    {
        private static MongoClient client = new MongoClient("mongodb://localhost:27017");
        private static IMongoDatabase db = client.GetDatabase("SpotHero");

        public static RateCollection getRates()
        {
            var rates = db.GetCollection<Rate>("Rates").Find<Rate>(FilterDefinition<Rate>.Empty);
            return new RateCollection()
            {
                rates = rates.ToList<Rate>()
            };
        }

        public static void updateRates(RateCollection rateCollection)
        {
            db.DropCollection("Rates");
            db.GetCollection<Rate>("Rates").InsertMany(rateCollection.rates);
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

            // clear existing data, then seed with sample data
            db.DropCollection("Rates");
            db.GetCollection<Rate>("Rates").InsertMany(new List<Rate>() { rate1, rate2, rate3, rate4, rate5 });
        }
    }
}
