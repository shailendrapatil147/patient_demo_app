using API.Common.BaseController;
using Demo.Api.Contracts.Models;
using Demo.API.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net;

namespace Demo.API.Controllers
{
    [ApiVersion("1.0")]
    public class ValuesController : BaseController
    {
        public IPatientManager _patientManager { get; set; }

        public ValuesController(IConfiguration configuration, IPatientManager ipatientManager) : base(configuration)
        {
            _patientManager = ipatientManager;
        }



        [HttpGet]
        [Produces("application/json")]
        [SwaggerOperation("Get")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<Patient>))]
        [Route("values")]
        // GET: api/values
        public IEnumerable<string> Get()
        {
            _patientManager.GetAllPatient();
            return new string[] { "value1", "value2" };
        }


    }
}
