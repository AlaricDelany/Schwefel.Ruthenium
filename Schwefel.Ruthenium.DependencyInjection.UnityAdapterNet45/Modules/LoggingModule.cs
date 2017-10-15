using Microsoft.Extensions.Logging;
using Schwefel.Ruthenium.DependencyInjection.Logging.Modules;
using System;
using System.Linq;

namespace Schwefel.Ruthenium.DependencyInjection.UnityAdapterNet45.Modules
{
    public class LoggingModule : IUnityModule, ILoggingModule, IDisposable
    {
        public LoggingModule(params OnConstructLoggerFactory[] constructingInformation)
        {
            if(constructingInformation == null)
                return;

            foreach (var ci in constructingInformation.Where(c => c != null))
            {
                OnConstructing += ci;
            }
        }

        public event OnConstructLoggerFactory OnConstructing;


        ~LoggingModule()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (OnConstructing != null)
            {
                var subscriber = OnConstructing.GetInvocationList().OfType<OnConstructLoggerFactory>().ToArray();

                foreach (var currentSub in subscriber)
                {
                    OnConstructing -= currentSub;
                }
            }
        }

        public void Configure(UnityDependencyInjectionContainer container)
        {
            ILoggerFactory factory = new LoggerFactory();

            OnConstructing?.Invoke(factory);

            container.RegisterInstance(factory);
        }
    }
}
