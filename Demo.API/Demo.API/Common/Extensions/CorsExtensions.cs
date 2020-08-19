using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.API.Common.Extensions
{
    public static class CorsExtensions
    {
        /// <summary>
        /// Applies the CORS policy defined in appsettings.config
        /// The appsettings should have values defined as per below format
        ///  "CorsSettings": {
        ///     "AllowedUrls" : [
        ///             "www.someallowedurl1.com",
        ///             "www.someallowedurl2.com"
        ///         ],
        ///     "AllowedMethods" : [
        ///             "GET",
        ///             "POST",
        ///             "PUT",
        ///             "OPTIONS"
        ///         ],
        ///     "AllowedHeaders" : [
        ///             "origin",
        ///             "pragma", 
        ///             "accept",
        ///             "content-type",
        ///             "authorization",
        ///             "cache-control",
        ///             "x-xpx-api-key",
        ///             "x-xpx-customerId",
        ///             "x-xpx-applicationId"
        ///         ]
        ///  }
        ///  
        ///  To Allow all use below settings
        ///  
        ///  "CorsSettings": {
        ///     "AllowedUrls" : [ "*" ],
        ///     "AllowedMethods" : [ "*" ],
        ///     "AllowedHeaders" : [ "*" ]
        ///  }
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        public static IApplicationBuilder UseCors(this IApplicationBuilder app, IConfiguration configuration)
        {
            var allowedUrls = configuration
                                .GetSection("CorsSettings:AllowedUrls")
                                .GetChildren()
                                .Select(x => x.Value.Trim())
                                .ToArray();

            var allowedHeaders = configuration
                                    .GetSection("CorsSettings:AllowedHeaders")
                                    .GetChildren()
                                    .Select(x => x.Value.Trim())
                                    .ToArray();

            var allowedMethods = configuration
                                    .GetSection("CorsSettings:AllowedMethods")
                                    .GetChildren()
                                    .Select(x => x.Value.Trim())
                                    .ToArray();

            if (allowedUrls?.Count() > 0 && allowedHeaders?.Count() > 0 && allowedMethods?.Count() > 0)
            {
                return app.UseCors(options =>
                                options.WithOrigins(allowedUrls)
                                        .WithHeaders(allowedHeaders)
                                        .WithMethods(allowedMethods));
            }
            else
            {
                throw new ApplicationException($"Cors policies are not defined properly in appsettings. Please check the documentation of {nameof(UseCors)}");
            }
        }
    }
}
