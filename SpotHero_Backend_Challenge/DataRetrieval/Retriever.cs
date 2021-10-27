using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace SpotHero_Backend_Challenge
{
    public class Retriever
    {
        private static MongoClient client = new MongoClient("mongodb://default-whatevs:DFrMN1f2ApCC3q0la7sZGIipAeEntn1DKs9uH4pBLH8z35yROCpD9M0qXGF9ZTuaQ5vAvSapxHQXUBXGYIRbaQ==@default-whatevs.mongo.cosmos.azure.com:10255/?ssl=true&retrywrites=false&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@default-whatevs@");  
        //"mongodb://localhost:27017");
        private static IMongoDatabase db = client.GetDatabase("SpotHero");

        public static ParkingRateCollection getRates()
        {
            var rates = db.GetCollection<ParkingRate>("Rates").Find<ParkingRate>(FilterDefinition<ParkingRate>.Empty);
            return new ParkingRateCollection()
            {
                rates = rates.ToList<ParkingRate>()
            };
        }

        public static void updateRates(ParkingRateCollection rateCollection)
        {
            db.DropCollection("Rates");
            db.GetCollection<ParkingRate>("Rates").InsertMany(rateCollection.rates);
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

            var rate1 = new ParkingRate()
            {
                days = "mon,tues,thurs",
                times = "0900-2100",
                tz = "America/Chicago",
                price = 1500
            };
            var rate2 = new ParkingRate()
            {
                days = "fri,sat,sun",
                times = "0900-2100",
                tz = "America/Chicago",
                price = 2000
            };
            var rate3 = new ParkingRate()
            {
                days = "wed",
                times = "0600-1800",
                tz = "America/Chicago",
                price = 1750
            };
            var rate4 = new ParkingRate()
            {
                days = "mon,wed,sat",
                times = "0100-0500",
                tz = "America/Chicago",
                price = 1000
            };
            var rate5 = new ParkingRate()
            {
                days = "sun,tues",
                times = "0100-0700",
                tz = "America/Chicago",
                price = 925
            };

            // clear existing data, then seed with sample data
            db.DropCollection("Rates");
            db.GetCollection<ParkingRate>("Rates").InsertMany(new List<ParkingRate>() { rate1, rate2, rate3, rate4, rate5 });
        }

        /// <summary>
        /// Validates rates by attempting to create rate instances for each rate
        /// </summary>
        /// <param name="rateCollection"></param>
        /// <returns>throws exception if invalid</returns>
        //private static bool validateRates(ParkingRateCollection rateCollection)
        //{
        //    foreach(ParkingRate rate in rateCollection.rates)
        //    {
        //        new ParkingRateInstance()
        //    }
        //}

    }
}
