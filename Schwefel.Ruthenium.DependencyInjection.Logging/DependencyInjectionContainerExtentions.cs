using Microsoft.Extensions.Logging;
using System;

namespace Schwefel.Ruthenium.DependencyInjection
{
    public static class DependencyInjectionContainerExtentions
    {
        public static ILoggerFactory GetLoggerFactory(this IDependencyInjectionContainer container)
        {
            if(container.IsRegistered<ILoggerFactory>())
            {
                return container.Resolve<ILoggerFactory>();
            }
            else
            {
                var result = new LoggerFactory();

                RegisterLoggerFactoryInternal(container, result);

                return result;
            }
        }


        private static void RegisterLoggerFactoryInternal(IDependencyInjectionContainer container, ILoggerFactory loggerFactory)
        {
            container.RegisterInstance(loggerFactory);
        }
    }
}
