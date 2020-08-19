using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Common.Error
{
    public class ApiErrorProvider
    {
        private IConfigurationSection _apiErrorsList;

        public ApiErrorProvider()
        {
        }

        public ApiErrorProvider(IConfiguration configuration)
        {
            _apiErrorsList = configuration.GetSection("ApiErrors");
        }

        public ErrorServiceResponse GetErrorResponse(string code, string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                return GetErrorResponse(code);
            }
            else
            {
                return new ErrorServiceResponse
                {
                    Errors = new List<ErrorResponse> { new ErrorResponse(code, description, null) }
                };
            }
        }

        public ErrorServiceResponse GetErrorResponse(ModelStateDictionary modelState)
        {
            return new ErrorServiceResponse
            {
                Errors = new List<ErrorResponse> { GetFieldErrorResponse(modelState) }
            };
        }

        public ErrorServiceResponse GetErrorResponse(string errorCode)
        {
            var response = new ErrorServiceResponse();
            var description = GetErrorDescription(errorCode);
            response.Errors = new List<ErrorResponse> { new ErrorResponse(errorCode, description, null) };
            return response;
        }

        public ErrorResponse GetFieldErrorResponse(ModelStateDictionary modelState)
        {
            var fieldErrors = FormatFieldErrors(modelState);
            var errors = fieldErrors.Select(e => new ErrorInfo(ConvertToApiJsonFormat(e.Field), e.Type, e.Detail));
            var response = new ErrorResponse("invalid_fields", "Invalid or missing fields", errors.ToList());
            return response;
        }

        private IEnumerable<ErrorInfo> FormatFieldErrors(ModelStateDictionary modelState)
        {
            var result = from ms in modelState
                         where ms.Value.Errors.Any()
                         let field = ms.Key
                         from error in ms.Value.Errors
                         let parts = error.ErrorMessage.Split(',')
                         let isException = string.IsNullOrEmpty(parts[0]) && error.Exception != null
                         let type = isException ? "Exception" : parts[0]
                         let detail = parts.Count() > 1 ? parts[1] : (isException ? "Error occured while validating field values." : parts[0])
                         select new ErrorInfo(field, type, detail);
            return result;
        }

        private string ConvertToApiJsonFormat(string field)
        {
            if (!string.IsNullOrEmpty(field))
            {
                //lowerCase first letter of each string
                if (field.Contains("."))
                {
                    var names = field.Split('.');

                    for (int i = 0; i < names.Count(); i++)
                    {
                        names[i] = char.ToLowerInvariant(names[i][0]) + names[i].Substring(1);
                    }
                    field = string.Join(".", names);
                }
            }
            return field;
        }

        private string GetErrorDescription(Exception ex)
        {
            return ex.InnerException == null ? ex.Message : GetErrorDescription(ex.InnerException);
        }

        private string GetErrorDescription(string errorCode)
        {
            var description = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(errorCode))
                {
                    // Get the error list from Service json config
                    description = _apiErrorsList[errorCode];
                }
            }
            catch (Exception)
            {
                description = string.Empty;
            }
            return description;
        }
    }
}
