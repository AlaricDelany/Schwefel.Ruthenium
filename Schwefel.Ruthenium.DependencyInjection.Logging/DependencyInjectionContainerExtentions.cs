using Microsoft.Extensions.Logging;
using Schwefel.Ruthenium.DependencyInjection.Logging.Modules;
using System;

namespace Schwefel.Ruthenium.DependencyInjection
{
    public static class DependencyInjectionContainerExtentions
    {
        public static ILoggerFactory GetLoggerFactory(this IDependencyInjectionContainer container)
        {
            return GetLoggerFactoryInternal(container);
        }

        public static ILogger<T> GetLogger<T>(this IDependencyInjectionContainer container)
            where T : class
        {
            var factory = GetLoggerFactoryInternal(container);

            return factory.CreateLogger<T>();
        }

        private static ILoggerFactory GetLoggerFactoryInternal(IDependencyInjectionContainer container)
        {
            if(!container.IsRegistered<ILoggerFactory>())
            {
                throw new InvalidOperationException($"Please Register the {nameof(ILoggingModule)} Module at the IOC.");
            }
            return container.Resolve<ILoggerFactory>();
        }
    }
}
