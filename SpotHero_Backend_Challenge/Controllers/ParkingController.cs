using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;

namespace SpotHero_Backend_Challenge
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingController : ControllerBase
    {

        //private readonly ILogger<ParkingController> _logger;

        //public ParkingController(ILogger<ParkingController> logger)
        //{
        //    _logger = logger;
        //}

        [SwaggerOperation("Retrieves all available parking rates")]
        [HttpGet("rates")]
        [SwaggerResponse(200, "Current rates successfully retrieved")]
        [SwaggerResponse(500, "Unspecified error")]
        public ParkingRateCollection Get()
        {
            ParkingRateCollection rateCollection = null;

            try
            {
                rateCollection = Retriever.GetRates();
            }
            catch
            {
                Response.StatusCode = 500;
            }

            return rateCollection;
        }

        [SwaggerOperation("Clears existing rates and sets a new collection of parking rates")]
        [HttpPut("rates")]
        [SwaggerResponse(200, "Rates successfully added")]
        [SwaggerResponse(500, "Unspecified error")]
        public void Put([FromBody] ParkingRateCollection rates)
        {
            try
            {
                Retriever.UpdateRates(rates);
            }
            catch
            {
                Response.StatusCode = 500;
            }
        }

        [SwaggerOperation("Retrieves the price of a parking spot available during the specified time frame.", "Uses ISO-8601 style date inputs. The `price-epoch` endpoint is recommended because ['time' can change](https://codeblog.jonskeet.uk/2019/03/27/storing-utc-is-not-a-silver-bullet/).")]
        [HttpGet("price")]
        [SwaggerResponse(404, "No parking available for the specified time frame")]
        [SwaggerResponse(200, "Price of parking for the specified time frame")]
        [SwaggerResponse(412, "Invalid input")]
        [SwaggerResponse(500, "Unspecified error")]
        public Price GetPrice(DateTime start, DateTime end)
        {
            Price price = null;

            try
            {
                price = RateMatcher.GetPrice(start, end);
                if (price == null) Response.StatusCode = 404; // request was fine, but no matches
            } 
            catch (ArgumentOutOfRangeException)
            {
                Response.StatusCode = 412; // precondition failed
            } 
            catch (Exception)
            {
                Response.StatusCode = 500; // some other problem
            }

            return price;
        }

       [SwaggerOperation("Retrieves the price of a parking spot available during the specified time frame, using epoch time and a standard timezone.", "This endpoint is recommended over the /price endpoint to avoid edge cases involving timezone-related changes.")]
       [HttpGet("price-epoch")]
       [SwaggerResponse(404, "No parking available for the specified time frame")]
       [SwaggerResponse(200, "Price of parking for the specified time frame")]
       [SwaggerResponse(412, "Invalid input")]
       [SwaggerResponse(500, "Unspecified error")]
        public Price GetPrice(int epochSecondsStart, int epochSecondsEnd, string ianaTimezone)
        {
            Price price = null;

            try
            {
                price = RateMatcher.GetPrice(epochSecondsStart, epochSecondsEnd, ianaTimezone);
                if (price == null) Response.StatusCode = 404; // request was fine, but no matches
            }
            catch (ArgumentOutOfRangeException)
            {
                Response.StatusCode = 412; // precondition failed
            }
            catch (Exception)
            {
                Response.StatusCode = 500; // some other problem
            }

            return price;
        }

        [HttpPut("reset")]
        [SwaggerOperation("Resets rates to the specified defaults", "Default rates: https://github.com/spothero/be-code-challenge#sample-json-for-testing")]
        [SwaggerResponse(200, "All data cleared")]
        [SwaggerResponse(500, "Unspecified error")]
        public void Reset()
        {
            try
            {
                Retriever.SeedRatesChicago();
            }
            catch
            {
                Response.StatusCode = 500;
            }
            
        }


    }
}
