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
        public Rate GetPrice(DateTime start, DateTime end)
        {
            // todo
            return Retriever.getRate(start, end);
        }

        [HttpPut("reset")]
        [SwaggerOperation("Resets rates per https://github.com/spothero/be-code-challenge#sample-json-for-testing")]
        public void Reset()
        {
            Retriever.seedRates();
        }


    }
}
