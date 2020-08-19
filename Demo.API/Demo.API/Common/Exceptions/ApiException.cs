
using System;
using System.Net;

namespace Demo.API.Common.Exceptions
{
    public class ApiException : Exception
    {
        public string Code { get; }
        public string Description { get; }
        public HttpStatusCode StatusCode { get; }
        public Exception Exception { get; }

        public ApiException(HttpStatusCode statusCode, string code, string description = null, Exception exception = null) : base()
        {
            StatusCode = statusCode;
            Code = code;
            Description = description;
            Exception = exception;
        }
    }
}
