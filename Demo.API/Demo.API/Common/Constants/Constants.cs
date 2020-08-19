using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.API.Common.Constants
{
    internal class Headers
    {
        public const string AUTHORIZE_FLAG = "x-authorize";
        public const string SESSION_ID = "x-sessionId";
        public const string CONTEXT_ID = "x-contextId";
        public const string CORRELATION_ID = "x-correlationId";
        public const string ROUTE = "route";
        public const string APPLICATION_ID = "x-applicationId";
    }

    internal class InfrastructureKeys
    {
        public const string VersionRegEx = @"\/v[0-9]*\/";
        public const string UrlPattern = "\"https:\\\\{baseutl}\v{versionNumber}\\{serviveName}\\{routeUrl}\"";
        public const string Url = "Url";
        public const string Api = "Api";
    }
}
