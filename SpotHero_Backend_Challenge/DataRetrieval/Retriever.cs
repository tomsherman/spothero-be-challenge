using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace SpotHero_Backend_Challenge
{
    /// <summary>
    /// Contains methods for maintaining rates in the database.
    /// </summary>
    public class Retriever
    {
        private readonly static MongoClient client = new MongoClient("mongodb://default-whatevs:DFrMN1f2ApCC3q0la7sZGIipAeEntn1DKs9uH4pBLH8z35yROCpD9M0qXGF9ZTuaQ5vAvSapxHQXUBXGYIRbaQ==@default-whatevs.mongo.cosmos.azure.com:10255/?ssl=true&retrywrites=false&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@default-whatevs@");  
        private readonly static IMongoDatabase db = client.GetDatabase("SpotHero");

        public static ParkingRateCollection GetRates()
        {
            // All rate documents in db. No filter.
            var rates = db.GetCollection<UnverifiedParkingRateInput>("Rates").Find<UnverifiedParkingRateInput>(FilterDefinition<UnverifiedParkingRateInput>.Empty);
            return new ParkingRateCollection(rates.ToList<UnverifiedParkingRateInput>());
        }

        public static void UpdateRates(ParkingRateCollection rateCollection)
        {
            if (rateCollection == null) throw new ArgumentNullException();
            if (rateCollection.rates == null) throw new ArgumentNullException();
            if (rateCollection.rates.Count == 0) throw new ArgumentException();

            // test validity of input rates by creating rate instances
            // if any rate is invalid, an exception is thrown, and the update will fail in its entirety
            foreach(UnverifiedParkingRateInput rate in rateCollection.rates)
            {
                VerifiedParkingRate.GetVerifiedRates(rate);
            }

            // idempotent
            // always clear before adding
            db.DropCollection("Rates");
            db.GetCollection<UnverifiedParkingRateInput>("Rates").InsertMany(rateCollection.rates);
        }

        /// <summary>
        /// Seeds the database with rates provided in the challenge.
        /// </summary>
        public static void SeedRatesChicago()
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

            var rate1 = new UnverifiedParkingRateInput()
            {
                days = "mon,tues,thurs",
                times = "0900-2100",
                tz = "America/Chicago",
                price = 1500
            };
            var rate2 = new UnverifiedParkingRateInput()
            {
                days = "fri,sat,sun",
                times = "0900-2100",
                tz = "America/Chicago",
                price = 2000
            };
            var rate3 = new UnverifiedParkingRateInput()
            {
                days = "wed",
                times = "0600-1800",
                tz = "America/Chicago",
                price = 1750
            };
            var rate4 = new UnverifiedParkingRateInput()
            {
                days = "mon,wed,sat",
                times = "0100-0500",
                tz = "America/Chicago",
                price = 1000
            };
            var rate5 = new UnverifiedParkingRateInput()
            {
                days = "sun,tues",
                times = "0100-0700",
                tz = "America/Chicago",
                price = 925
            };

            var sampleRates = new ParkingRateCollection(new List<UnverifiedParkingRateInput>() { rate1, rate2, rate3, rate4, rate5 });
            UpdateRates(sampleRates);
        }

        /// <summary>
        /// Seeds the database with test data in Cairo's timezone
        /// </summary>
        public static void SeedRatesCairo()
        {
            var rate1 = new UnverifiedParkingRateInput()
            {
                days = "mon,tues,thurs",
                times = "0900-2100",
                tz = "Africa/Cairo",
                price = 1500
            };
            var rate2 = new UnverifiedParkingRateInput()
            {
                days = "fri,sat,sun",
                times = "0900-2100",
                tz = "Africa/Cairo",
                price = 2000
            };
            var rate3 = new UnverifiedParkingRateInput()
            {
                days = "wed",
                times = "0600-1800",
                tz = "Africa/Cairo",
                price = 1750
            };
            var rate4 = new UnverifiedParkingRateInput()
            {
                days = "mon,wed,sat",
                times = "0100-0500",
                tz = "Africa/Cairo",
                price = 1000
            };
            var rate5 = new UnverifiedParkingRateInput()
            {
                days = "sun,tues",
                times = "0100-0700",
                tz = "Africa/Cairo",
                price = 925
            };

            // var sampleRates = new ParkingRateCollection(new List<UnverifiedParkingRateInput>() { rate1, rate2, rate3, rate4, rate5 });
            var sampleRates = new ParkingRateCollection(new List<UnverifiedParkingRateInput>() { rate3 });
            UpdateRates(sampleRates);
        }
    }
}
