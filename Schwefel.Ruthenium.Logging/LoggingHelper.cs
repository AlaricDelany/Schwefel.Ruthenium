using Microsoft.Extensions.Logging;

namespace Schwefel.Ruthenium.Logging
{
    public static class LoggingHelper
    {
        public static ILoggerFactory LoggerFactory { get; } = new LoggerFactory();

        public static ILogger CreateLogger<T>() =>
            LoggerFactory.CreateLogger<T>();
    }
}
