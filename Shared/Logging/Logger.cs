using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Shared.Logging
{
    public static class Logger
    {
        private static ILoggerFactory? _loggerFactory;

        public static void ConfigureLogger(IServiceProvider serviceProvider)
        {
            _loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        }

        public static ILogger CreateLogger<T>() where T : class
        {
            if (_loggerFactory == null)
            {
                throw new InvalidOperationException("LoggerFactory is not configured. Call ConfigureLogger first.");
            }

            return _loggerFactory.CreateLogger<T>();
        }

        public static ILogger CreateLogger(string categoryName)
        {
            if (_loggerFactory == null)
            {
                throw new InvalidOperationException("LoggerFactory is not configured. Call ConfigureLogger first.");
            }

            return _loggerFactory.CreateLogger(categoryName);
        }
    }
}
