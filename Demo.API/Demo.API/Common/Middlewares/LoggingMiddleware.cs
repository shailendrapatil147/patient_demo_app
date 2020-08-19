using Demo.API.Common.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Demo.API.Common.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate next;

        public LoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, ILoggingService loggingService)
        {
            try
            {
                var stopwatch = new Stopwatch();

                StringValues contextId = GetContextId(context);

                StringValues correlationId = GetCorrelationId(context, contextId);

                StringValues applicationId = GetApplicationId(context);


                ConcurrentDictionary<string, object> dict = GetDictionary(contextId, correlationId, applicationId);

                if (context.Request.Path.HasValue)
                {
                    dict.TryAdd(Constants.Headers.ROUTE, context.Request.Path.Value);
                }

                string ipV4Address = context.Connection?.RemoteIpAddress?.MapToIPv4().ToString();

                using (loggingService.ConfigureLoggingAsync(dict))
                {
                    stopwatch.Start();

                    var logger = loggingService.GetLogger<LoggingMiddleware>(nameof(LoggingMiddleware));

                    using (logger.BeginScope(new Dictionary<string, object> { ["clientIpAddress"] = ipV4Address }))
                    {
                        logger.LogInformation($"Processing Http request from URL: {context.Request.Path.Value}");
                    }

                    await next(context).ConfigureAwait(false);

                    stopwatch.Stop();

                    logger.LogInformation("Finished processing Http request to URL: {URL}. HttpMethod: {HttpMethod}. Duration: {RequestDuration}",
                        context.Request.Path.Value, context.Request.Method, stopwatch.ElapsedMilliseconds);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private ConcurrentDictionary<string, object> GetDictionary(StringValues contextId, StringValues correlationId, StringValues applicationId)
        {
            ConcurrentDictionary<string, object> dict = new ConcurrentDictionary<string, object>();

            if (!string.IsNullOrEmpty(contextId))
            {
                dict.TryAdd(Constants.Headers.CONTEXT_ID, contextId);
            }
            if (!string.IsNullOrEmpty(correlationId))
            {
                dict.TryAdd(Constants.Headers.CORRELATION_ID, correlationId);
            }
            if (!string.IsNullOrEmpty(applicationId))
            {
                dict.TryAdd(Constants.Headers.APPLICATION_ID, applicationId);
            }

            return dict;
        }

        private StringValues GetApplicationId(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(Constants.Headers.APPLICATION_ID, out StringValues applicationId))
            {
                applicationId = "UnknownApplication";
            }

            return applicationId;
        }

        private StringValues GetCorrelationId(HttpContext context, StringValues contextId)
        {
            StringValues correlationId;
            // test for correlation id
            bool res = context.Request.Headers.TryGetValue(Constants.Headers.CORRELATION_ID, out correlationId);
            if (!res)
            {
                // correlation id not found
                // use context id to populate correlation id header
                correlationId = contextId;
                context.Request.Headers.Add(Constants.Headers.CORRELATION_ID, correlationId);
            }
            else if (string.IsNullOrWhiteSpace(correlationId))
            {
                // correlation id is found but it consists of white spaces
                // remove it first
                context.Request.Headers.Remove(Constants.Headers.CORRELATION_ID);
                // and add generated guid
                correlationId = contextId;
                context.Request.Headers.Add(Constants.Headers.CORRELATION_ID, correlationId);
            }

            return correlationId;
        }

        private StringValues GetContextId(HttpContext context)
        {
            StringValues contextId;

            bool res = context.Request.Headers.TryGetValue(Constants.Headers.CONTEXT_ID, out contextId);
            if (!res)
            {
                // If context id is not found in the header, create new one and add it to request headers
                contextId = Guid.NewGuid().ToString().Replace("-", "");
                context.Request.Headers.Add(Constants.Headers.CONTEXT_ID, contextId);
            }
            else if (string.IsNullOrWhiteSpace(contextId))
            {
                // context id is found but it consists of white spaces
                // remove it first
                context.Request.Headers.Remove(Constants.Headers.CONTEXT_ID);
                // and generate new guid
                contextId = Guid.NewGuid().ToString().Replace("-", "");
                context.Request.Headers.Add(Constants.Headers.CONTEXT_ID, contextId);
            }

            return contextId;
        }
    }
}
