using Demo.API.Common.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Demo.API.Common.Extensions
{
    public static class LoggingExtensions
    {
        public static IApplicationBuilder UseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}
