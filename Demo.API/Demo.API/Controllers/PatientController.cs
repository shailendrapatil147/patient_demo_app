using API.Common.BaseController;
using Demo.Api.Contracts.Models;
using Demo.API.Common.Logging;
using Demo.API.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net;

namespace Demo.API.Controllers
{
    [ApiVersion("1.0")]
    public class PatientController : BaseController
    {
        private IPatientManager _patientManager { get; set; }

        public PatientController(IConfiguration configuration,
            ILoggingService loggingService,
            IPatientManager patientManager) : base(configuration)
        {
            _logger = loggingService.GetLogger<PatientController>(nameof(PatientController));
            _patientManager = patientManager;
        }

        [HttpGet]
        [Produces("application/json")]
        [SwaggerOperation("Get")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<Patient>))]
        [Route("patient/GetAllPatient")]
        public IActionResult GetAllPatient()
        {
            _logger.LogInformation($"Started processing Get: GetAllPatient.");

            var response = _patientManager.GetAllPatient();

            IActionResult msg = CreateResponse(HttpStatusCode.OK, response);
            _logger.LogInformation($"Completed processing Get: GetAllPatient.");

            return msg;
        }

        [HttpGet]
        [Produces("application/json")]
        [SwaggerOperation("Get")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Patient))]
        [Route("patienr/GetPatientById")]
        public IActionResult GetPatientById(int PatientId)
        {
            _logger.LogInformation($"Started processing Get: GetPatientById.");

            var response = _patientManager.GetPatientById(PatientId);

            IActionResult msg = CreateResponse(HttpStatusCode.OK, response);
            _logger.LogInformation($"Completed processing Get: GetPatientById.");

            return msg;
        }

        [HttpPost]
        [Produces("application/json")]
        [SwaggerOperation("Post")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(bool))]
        [Route("patient/SavePatient")]
        public IActionResult SavePatient([FromBody] Patient Patient)
        {
            _logger.LogInformation($"Started processing Post: SavePatient.");

            _patientManager.SavePatient(Patient);

            IActionResult msg = CreateResponse(HttpStatusCode.Created, new { isSuccess= true});
            _logger.LogInformation($"Completed processing Post: SavePatient.");

            return msg;
        }
    }
}
