using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Logging
{
    public class CustomLoggerFactory : ILoggerProvider
    {

        public ILogger CreateLogger(string categoryName)
        {
            return new CustomLogger();
        }

        public void Dispose()
        {
        }
    }

    public class CustomLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {

            if (!IsEnabled(logLevel))
            {
                return;
            }

            string logMessage = formatter(state, exception);
            string fullFilePath = "log.txt";

            logMessage = $"[{DateTime.Now:dd-MM-yyyy HH:mm:ss}]-{logMessage}";

            using (var streamWriter = new StreamWriter(fullFilePath, true))
            {
                streamWriter.WriteLine(logMessage);
                streamWriter.Close();
            }

        }
    }
}
