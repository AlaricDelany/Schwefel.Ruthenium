using Microsoft.Extensions.Logging;
using Schwefel.Ruthenium.DependencyInjection.Modules;
using System;

namespace Schwefel.Ruthenium.DependencyInjection.Logging.Modules
{
    public delegate void OnConstructLoggerFactory(ILoggerFactory loggerFactory);

    public interface ILoggingModule : IModule
    {
        event OnConstructLoggerFactory OnConstructing;
    }
}
