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
        private static ConsoleAppLogger _instance;
        private static Logger _logger;
        public ConsoleAppLogger GetInstance()
        {
            if (_instance == null) _instance = new ConsoleAppLogger();
            return _instance;
        }
        private Logger GetLogger()
        {
            if (_logger == null) _logger = LogManager.GetCurrentClassLogger();
            return _logger;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException(); // i don't know how to implement this for nlog
        }

        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            switch (logLevel)
            {
                case Microsoft.Extensions.Logging.LogLevel.Trace:
                    return GetLogger().IsTraceEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Debug:
                    return GetLogger().IsDebugEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Information:
                    return GetLogger().IsInfoEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Error:
                    return GetLogger().IsErrorEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Warning:
                    return GetLogger().IsWarnEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Critical:
                    return GetLogger().IsFatalEnabled;
                default: return false;
            }
        }

        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            switch (logLevel)
            {
                case Microsoft.Extensions.Logging.LogLevel.Trace:
                    if (exception != null)
                        GetLogger().Trace($"{exception} {state}");
                    else GetLogger().Trace(state);
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Debug:
                    if (exception != null)
                        GetLogger().Debug($"{exception} {state}");
                    else GetLogger().Debug(state);
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Information:
                    if (exception != null)
                        GetLogger().Info($"{exception} {state}");
                    else GetLogger().Info(state);
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Error:
                    if (exception != null)
                        GetLogger().Error($"{exception} {state}");
                    else GetLogger().Error(state);
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Warning:
                    if (exception != null)
                        GetLogger().Warn($"{exception} {state}");
                    else GetLogger().Warn(state);
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Critical:
                    if (exception != null)
                        GetLogger().Fatal($"{exception} {state}");
                    else GetLogger().Fatal(state);
                    break;
            }
        }
    }
}
