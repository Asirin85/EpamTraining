using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NLog;
using StringToIntLib;

namespace ConsoleUI
{
    class ConsoleAppLogger : NLog.Logger, Microsoft.Extensions.Logging.ILogger<StringLib>
    {
        private Logger _logger;
        public ConsoleAppLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return NLog.MappedDiagnosticsLogicalContext.SetScoped("scope", state.ToString());
        }

        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel) => logLevel switch
        {
            Microsoft.Extensions.Logging.LogLevel.Trace => _logger.IsTraceEnabled,
            Microsoft.Extensions.Logging.LogLevel.Debug => _logger.IsDebugEnabled,
            Microsoft.Extensions.Logging.LogLevel.Information => _logger.IsInfoEnabled,
            Microsoft.Extensions.Logging.LogLevel.Error => _logger.IsErrorEnabled,
            Microsoft.Extensions.Logging.LogLevel.Warning => _logger.IsWarnEnabled,
            Microsoft.Extensions.Logging.LogLevel.Critical => _logger.IsFatalEnabled,
            { } => false,
        };

        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            switch (logLevel)
            {
                case Microsoft.Extensions.Logging.LogLevel.Trace:
                    if (exception != null)
                        _logger.Trace($"{exception} {state}");
                    else _logger.Trace(state);
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Debug:
                    if (exception != null)
                        _logger.Debug($"{exception} {state}");
                    else _logger.Debug(state);
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Information:
                    if (exception != null)
                        _logger.Info($"{exception} {state}");
                    else _logger.Info(state);
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Error:
                    if (exception != null)
                        _logger.Error($"{exception} {state}");
                    else _logger.Error(state);
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Warning:
                    if (exception != null)
                        _logger.Warn($"{exception} {state}");
                    else _logger.Warn(state);
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Critical:
                    if (exception != null)
                        _logger.Fatal($"{exception} {state}");
                    else _logger.Fatal(state);
                    break;
            }
        }
    }
}
