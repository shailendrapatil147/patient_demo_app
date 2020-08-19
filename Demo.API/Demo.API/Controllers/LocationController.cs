using API.Common.BaseController;
using Demo.Api.Contracts.Models;
using Demo.API.Common.Logging;
using Demo.API.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net;

namespace Demo.API.Controllers
{
    [ApiVersion("1.0")]
    public class LocationController : BaseController
    {
        private ILocationManager _locationManager { get; set; }

        public LocationController(IConfiguration configuration,
            ILoggingService loggingService,
            ILocationManager locationManager) : base(configuration)
        {
            _logger = loggingService.GetLogger<LocationController>(nameof(LocationController));

            _locationManager = locationManager;
        }

        /// <summary>
        /// Get All Cities
        /// </summary>
        [HttpGet]
        [Produces("application/json")]
        [SwaggerOperation("Get: AllCities")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<City>))]
        [Route("location/GetAllCities")]
        public IActionResult GetAllCities()
        {
            _logger.LogInformation($"Started processing Get: GetAllCities.");

            var response = _locationManager.GetAllCities();

            IActionResult msg = CreateResponse(HttpStatusCode.OK, response);
            _logger.LogInformation($"Completed processing Get: GetAllCities.");

            return msg;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation("Get")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<State>))]
        [Route("location/GetAllStates")]
        public IActionResult GetAllStates()
        {
            _logger.LogInformation($"Started processing Get: GetAllStates.");

            var response = _locationManager.GetAllStates();

            IActionResult msg = CreateResponse(HttpStatusCode.OK, response);
            _logger.LogInformation($"Completed processing Get: GetAllStates.");

            return msg;
        }


    }
}
