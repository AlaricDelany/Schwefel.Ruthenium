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
            foreach (var ci in constructingInformation?.Where(c => c != null))
            {
                OnConstructing += ci;
            }
        }

        private OnConstructLoggerFactory _OnConstructing;

        public event OnConstructLoggerFactory OnConstructing
        {
            add
            {
                _OnConstructing += value;
            }
            remove
            {
                _OnConstructing -= value;
            }
        }

        ~LoggingModule()
        {
            Dispose();
        }

        public void Dispose()
        {
            var subscriber = _OnConstructing?.GetInvocationList().OfType<OnConstructLoggerFactory>().ToArray();

            if (subscriber != null)
            {
                foreach (var currentSub in subscriber)
                {
                    OnConstructing -= currentSub;
                }
            }
        }

        public void Configure(UnityDependencyInjectionContainer container)
        {
            ILoggerFactory factory = new LoggerFactory();

            _OnConstructing?.Invoke(factory);

            container.RegisterInstance(factory);
        }
    }
}
