using Microsoft.Extensions.Logging;

namespace Schwefel.Ruthenium.Logging
{
    public static class LoggingHelper
    {
        public static ILoggerFactory LoggerFactory { get; } = new LoggerFactory();

        public static ILogger<T> CreateLogger<T>() =>
            LoggerFactory.CreateLogger<T>();
    }
}
