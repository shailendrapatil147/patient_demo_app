using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace Demo.API.Common.Swagger
{
    public class SwaggerDefaultValues : IOperationFilter
    {
        /// <summary>
        /// Applies the filter to the specified operation using the given context.
        /// </summary>
        /// <param name="operation">The operation to apply the filter to.</param>
        /// <param name="context">The current operation filter context.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/412
            // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/pull/413
            foreach (var parameter in operation.Parameters)
            {
                var description = context.ApiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);
                var routeInfo = description.RouteInfo;

                if (string.IsNullOrEmpty(parameter.Name))
                {
                    parameter.Name = description.ModelMetadata?.Name;
                }

                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                if (parameter.Schema.Default == null && description.DefaultValue != null)
                {
                    parameter.Schema.Default = new OpenApiString(description.DefaultValue.ToString());
                }

                if (routeInfo == null)
                {
                    continue;
                }

                parameter.Required |= !routeInfo.IsOptional;
            }

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "x-xpx-customerid",
                In = ParameterLocation.Header,
                Description = "Customer id",
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "x-xpx-authorize",
                In = ParameterLocation.Header,
                Description = "Authorization header",
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "boolean",
                    Default = new OpenApiBoolean(false)
                }
            });
        }
    }
}
