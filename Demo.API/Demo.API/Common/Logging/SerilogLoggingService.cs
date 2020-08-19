using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Context;
using Serilog.Enrichers;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.RollingFile;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Demo.API.Common.Logging
{
    public class SerilogLoggingService : ILoggingService
    {
        private IConfiguration _configuration { get; set; }
        private IConfigurationSection _loggingConfigSection;
        public ILogger _logger { get; private set; }
        private ILoggerFactory _loggerFactory { get; set; }

        public SerilogLoggingService(IConfiguration configuration)
        {
            _loggingConfigSection = configuration.GetSection("Serilog");
            _configuration = configuration;

            CreateLoggerConfiguration();
            _logger = GetLogger<SerilogLoggingService>(configuration["ServiceSetting:ServiceName"]);

            _logger.LogInformation("Logging path '{Path}, Log Level '{LogLevel}'",
                _loggingConfigSection["PathFormat"],
                _loggingConfigSection["MinimumLevel"]);
        }

        private void CreateLoggerConfiguration(IDictionary<string, object> contextProperties = null)
        {
            long? fileSizeLimitBytes = null;
            int? retainedFileCountLimit = null;

            // If "fileSizeLimitBytes" element not found - keep fileSizeLimitBytes = null,
            // This implies default that is 1Gb size
            if (long.TryParse(_loggingConfigSection["fileSizeLimitBytes"], out long limit))
            {
                fileSizeLimitBytes = limit;
            }

            // If "retainedFileCountLimit" element not found - keep retainedFileCountLimit = null,
            // This implies default that is 31 files
            if (int.TryParse(_loggingConfigSection["retainedFileCountLimit"], out int countLimit))
            {
                retainedFileCountLimit = countLimit;
            }

            LoggerConfiguration config = new LoggerConfiguration()
                .ReadFrom.Configuration(_configuration)
                .WriteTo.Sink(new RollingFileSink(_loggingConfigSection["pathFormat"],
                        new JsonFormatter(renderMessage: true),
                        fileSizeLimitBytes,
                        retainedFileCountLimit),
                    (LogEventLevel)Enum.Parse(typeof(LogEventLevel),
                        _loggingConfigSection["MinimumLevel"]))
                .Enrich.WithMachineName()
                .Enrich.WithProperty("ApplicationName", _configuration["ServiceSetting:ServiceName"])
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithProcessId()
                .Enrich.WithProperty("SourceContext", string.Empty)
                .Enrich.With(new ThreadIdEnricher(), new MachineNameEnricher())
                // this ensures that calls to LogContext.PushProperty will cause the logger to be enriched
                .Enrich.FromLogContext();

            // If "logConsoleEnabled" element is not found - dont log to console
            if (bool.TryParse(_loggingConfigSection["logConsoleEnabled"], out bool consoleOutputEnabled) == false)
            {
                consoleOutputEnabled = false;
            }
            
            Log.Logger = config.CreateLogger();

            _loggerFactory = new LoggerFactory()
                .AddSerilog();
        }

        public void Shutdown()
        {
            //if (_telemetryClient != null)
            //{
            //    _telemetryClient.Flush();
            //}
        }

        public ILogger GetLogger<TSource>(string sourceContext, IDictionary<string, object> contextProperties = null)
        {
            if (contextProperties != null)
            {
                foreach (KeyValuePair<string, object> prop in contextProperties)
                {
                    Log.Logger = Log.Logger.ForContext(prop.Key, prop.Value);
                }
            }

            return _loggerFactory?.CreateLogger<TSource>();
        }

        public Task ConfigureLoggingAsync(ConcurrentDictionary<string, object> dict)
        {
            if (dict != null)
            {
                foreach (KeyValuePair<string, object> pair in dict)
                {
                    LogContext.PushProperty(pair.Key, pair.Value);
                }
                return Task.FromResult(0);
            }

            throw new Exception();
        }
    }
}
