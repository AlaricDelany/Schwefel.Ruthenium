using Microsoft.Extensions.Logging;
using Schwefel.Ruthenium.DependencyInjection.Logging.Modules;
using System;

namespace Schwefel.Ruthenium.DependencyInjection
{
    public static class DependencyInjectionContainerExtentions
    {
        public static ILoggerFactory GetLoggerFactory(this IDependencyInjectionContainer container)
        {
            if(!container.IsRegistered<ILoggerFactory>())
            {
                throw new InvalidOperationException($"Please Register the {nameof(ILoggingModule)} Module at the IOC.");
            }
            return container.Resolve<ILoggerFactory>();
        }
    }
}
