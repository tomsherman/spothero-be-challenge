using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;

namespace SpotHero_Backend_Challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingController : ControllerBase
    {

        private readonly ILogger<ParkingController> _logger;

        public ParkingController(ILogger<ParkingController> logger)
        {
            _logger = logger;
        }

        [SwaggerOperation("todo")]
        [HttpPut("rates")]
        public void Put([FromBody] List<Rate> rates)
        {
            Retriever.updateRates(rates);
        }

        [SwaggerOperation("todo")]
        [HttpGet("rates")]
        public List<Rate> Get()
        {
            return Retriever.getRates();
        }

        [SwaggerOperation("todo")]
        [HttpGet("price")]
        public Price GetPrice(DateTime start, DateTime end)
        {
            Price price = null;

            try
            {
                price = RateMatcher.getPrice(start, end);
            } 
            catch (ArgumentOutOfRangeException ex)
            {
                Response.StatusCode = 412; // precondition failed
            } 
            catch (Exception ex)
            {
                Response.StatusCode = 500;
            }

            return price;
        }

        [HttpPut("reset")]
        [SwaggerOperation("Resets rates per https://github.com/spothero/be-code-challenge#sample-json-for-testing")]
        public void Reset()
        {
            Retriever.seedRates();
        }


    }
}
