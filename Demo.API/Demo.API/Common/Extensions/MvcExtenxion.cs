using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Buffers;
using System.Collections.Generic;

namespace Demo.API.Common.Extensions
{
    public static class MvcExtenxion
    {
        /// <summary>
        /// This method adds MVC Core to service collection.
        /// Adds HandleExceptionAttribute using AddMvcOptions.
        /// Adds AddNewtonsoftJson with default serializer settings as converters, handlers, formatter and resolver
        /// </summary>
        public static IMvcCoreBuilder AddMvcCore(this IServiceCollection services, List<JsonConverter> jsonConverters)
        {
            return services.AddMvcCore()
                .AddApiExplorer()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.Converters = jsonConverters;
                });
        }
    }
}
