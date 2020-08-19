using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.API.Common.Logging
{
    public interface ILoggingService
    {
        ILogger _logger { get; }

        ILogger GetLogger<TSource>(string sourceContext, IDictionary<string, object> contextProperties = null);

        Task ConfigureLoggingAsync(ConcurrentDictionary<string, object> dict);

        void Shutdown();
    }
}
