using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

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
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [JsonIgnore]
        public string id { get; set; }
        public string days { get; set; }
        public string times { get; set; }
        public string tz { get; set; }
        public int price { get; set; }
    }
}
