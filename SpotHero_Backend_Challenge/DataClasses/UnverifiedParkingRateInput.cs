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

    /// <summary>
    /// This class represents user input. It is not yet verified; for example, the data may contain typos or violations of business logic. 
    /// </summary>
    /// <remarks>
    /// The fundamental idea here is to represent unverified, external data. The external source might be an HTTP PUT, or it might be the 
    /// database. Just because the data is in the database does not mean it's valid. 
    /// </remarks>
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

        /// <summary>
        /// Times in "HHMM-HHMM" format
        /// </summary>
        /// <remarks>Example: 0900-1600 represents 9 AM to 4 PM</remarks>
        [Required]
        public string times { get; set; }

        /// <summary>
        /// Timezone in IANA format
        /// </summary>
        [Required]
        public string tz { get; set; }

        /// <summary>
        /// Price. Must be greater than zero.
        /// </summary>
        [Required]
        public int price { get; set; }
    }
}
