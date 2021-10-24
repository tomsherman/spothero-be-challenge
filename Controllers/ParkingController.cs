using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public void Put([FromBody] List<Rate> rate)
        {
            // todo
        }

        [SwaggerOperation("todo")]
        [HttpGet("rates")]
        public List<Rate> Get()
        {
            // todo
            return new List<Rate>() { new Rate(), new Rate(), new Rate() };
        }

        [SwaggerOperation("todo")]
        [HttpGet("price")]
        public Rate GetPrice()
        {
            // todo
            return new Rate();
        }


    }
}
