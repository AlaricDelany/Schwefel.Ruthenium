using Microsoft.Extensions.Logging;

namespace Schwefel.Ruthenium.Logging
{
    public static class LogExtentions
    {
        public static ILogger<T> GetLogger<T>(this T self) => LoggingHelper.CreateLogger<T>();
    }
}
