using API.Common.Error;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System.Linq;
using System.Net;

namespace API.Common.BaseController
{
    public abstract class BaseController : Controller
    {
        protected ApiErrorProvider ApiErrorProvider { get; private set; }
        protected ILogger _logger { get; set; }

        public BaseController(IConfiguration configuration)
        {
            ApiErrorProvider = new ApiErrorProvider(configuration);
        }

        public BaseController()
        {
            ApiErrorProvider = new ApiErrorProvider();
        }

        protected string HeaderValue(string key)
        {
            if (!Request.Headers.TryGetValue(key, out StringValues result))
            {
                return null;
            }
            return result.FirstOrDefault();
        }

        protected IActionResult NoContentResponse()
            => new NoContentResult();

        protected IActionResult InternalServerResponse()
            => CreateErrorResponse(HttpStatusCode.InternalServerError, errorCode: HttpStatusCode.InternalServerError.ToString(), message: string.Empty);

        protected IActionResult BadRequestResponse()
            => new BadRequestObjectResult(ModelState);

        public IActionResult CreateErrorResponse(HttpStatusCode httpStatusCode, string errorCode, string message)
            => CreateErrorResponse(httpStatusCode, ApiErrorProvider.GetErrorResponse(errorCode, message));

        public IActionResult CreateErrorResponse(HttpStatusCode httpStatusCode, ErrorServiceResponse errorResponse)
            => CreateResponse(httpStatusCode, errorResponse);

        public IActionResult CreateResponse(HttpStatusCode httpStatusCode, object response)
        {
            HttpContext.Response.StatusCode = (int)httpStatusCode;
            return new ObjectResult(response);
        }
    }
}
