using API.Common.Error;
using Demo.API.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Demo.API.Common.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        #region Private Memebers
        private readonly RequestDelegate next;
        #endregion Private Members

        #region Constructor
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        #endregion Constructor 

        #region Public Methods
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        #endregion Public Methods

        #region Private Methods
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var result = GetErrorResponse(exception);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)GetHttpStatusCode(exception);

            return context.Response.WriteAsync(result);
        }

        private HttpStatusCode GetHttpStatusCode(Exception exception)
        {
            if (exception is ApiException)
            {
                return (exception as ApiException).StatusCode;
            }
            else
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        private string GetErrorResponse(Exception exception)
        {
            if (exception is ApiException)
            {
                var apiException = exception as ApiException;
                return JsonService.SerializeObject(new ErrorResponse(apiException.Code, apiException.Description ?? string.Empty, ex: apiException.Exception));
            }
            else
            {
                return JsonService.SerializeObject(new ErrorResponse("internal_server_error", string.Empty, ex: exception));
            }
        }
        #endregion Private Methods
    }
}
