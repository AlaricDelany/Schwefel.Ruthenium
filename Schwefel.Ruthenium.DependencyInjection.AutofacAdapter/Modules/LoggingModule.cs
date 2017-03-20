using Autofac;
using Microsoft.Extensions.Logging;
using Schwefel.Ruthenium.DependencyInjection.Logging.Modules;
using System;
using System.Linq;

namespace Schwefel.Ruthenium.DependencyInjection.AutofacAdapter.Modules
{
    public class LoggingModule : Module, IAutofacModule, ILoggingModule, IDisposable
    {
        public LoggingModule(params OnConstructLoggerFactory[] constructingInformation)
        {
            constructingInformation = constructingInformation?.Where(c => c != null).ToArray();

            if(constructingInformation == null || constructingInformation.Length <= 0)
                return;
            
            foreach(var ci in constructingInformation)
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

        protected override void Load(ContainerBuilder builder)
        {
            ILoggerFactory factory = new LoggerFactory();

            _OnConstructing?.Invoke(factory);

            builder.RegisterInstance(factory);

            base.Load(builder);
        }

        public void Dispose()
        {
            var subscriber = _OnConstructing?.GetInvocationList().OfType<OnConstructLoggerFactory>().ToArray();

            if(subscriber != null)
            {
                foreach(var currentSub in subscriber)
                {
                    OnConstructing -= currentSub;
                }
            }
        }
    }
}
