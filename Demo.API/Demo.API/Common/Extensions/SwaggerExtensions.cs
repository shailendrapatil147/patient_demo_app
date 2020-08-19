using Demo.API.Common.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Demo.API.Common.Extensions
{
    public static class SwaggerExtensions
    {
        /// <summary>
        /// This method will add versioning of API endpoint
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            var app = PlatformServices.Default.Application;
            return services.AddSwaggerGen(options =>
            {
                // resolve the IApiVersionDescriptionProvider service
                // note: that we have to build a temporary service provider here because one has not been created yet
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                // add a swagger document for each discovered API version
                // note: you might choose to skip or document deprecated API versions differently
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
                }

                // add a custom operation filter which sets default values
                options.OperationFilter<SwaggerDefaultValues>();

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = $"Demo API {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = "This is Demo API.",
                Contact = new OpenApiContact() { Name = "Shailendra Patil", Email = "ShailendraPatil147@gmail.com" },
            };

            if (description.IsDeprecated)
            {
                info.Description += "This API version has been deprecated.";
            }

            return info;
        }

        /// <summary>
        /// Before you use this method, create below swagger settings to appsettings.json
        /// Swagger : {
        ///     "Title":"Tile of your service",
        ///     "Description" : "Short description about the service",
        ///     "Contact" : {
        ///         "Name" : "Name of the team/person to contact",
        ///         "Email" : "Email of the team person to contact"
        ///     }
        /// }
        /// </summary>
        /// <param name="app"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            // Add the Swagger UI

            return app.UseSwagger()
                        .UseSwaggerUI(options =>
                        {
                            // build a swagger endpoint for each discovered API version
                            foreach (var description in provider.ApiVersionDescriptions)
                            {
                                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
                                options.RoutePrefix = "swagger/ui";
                            }
                        });
        }
    }
}
