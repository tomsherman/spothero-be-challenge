using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace SpotHero_Backend_Challenge
{
    // https://github.com/spothero/be-code-challenge#sample-json-for-testing
    //{
    //    "days": "mon,tues,thurs",
    //    "times": "0900-2100",
    //    "tz": "America/Chicago",
    //    "price": 1500
    //}
    public class UnverifiedParkingRateInput
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [JsonIgnore]
        public string id { get; set; }

        /// <summary>
        /// Comma-delimited list of days
        /// </summary>
        /// <remarks>Valid values: mon,tues,wed,thurs,fri,sat,sun (note non-standard</remarks>
        [Required]
        public string days { get; set; }
        [Required]
        public string times { get; set; }
        [Required]
        public string tz { get; set; }
        [Required]
        public int price { get; set; }
    }
}
