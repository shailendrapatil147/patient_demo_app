using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace API.Common.Error
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ErrorResponse
    {
        public ErrorResponse(string code, string message, IEnumerable<ErrorInfo> errors = null, Exception ex = null)
        {
            Code = code;
            Message = message;
            FieldErrors = errors;
            Exception = ex;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "fieldErrors", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<ErrorInfo> FieldErrors { get; set; }

        [JsonProperty(PropertyName = "exception", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Exception Exception { get; set; }
    }
}
