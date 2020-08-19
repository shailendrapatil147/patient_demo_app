using Demo.API.Common.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Demo.API.Common.Extensions
{
    public static class ExceptionExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
