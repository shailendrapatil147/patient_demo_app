using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.API.Common.Extensions
{
    public static class ApiVersionExtensions
    {
        /// <summary>
        /// This method will add versioning of API endpoint
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddHeaderVersioning(this IServiceCollection services)
        {
            return services.AddApiVersioning(o =>
            {
                o.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
            })
            .AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
        }
    }
}
