using Schwefel.Ruthenium.DependencyInjection.Modules;
using System;

namespace Schwefel.Ruthenium.DependencyInjection
{
    public interface IDependencyInjectionContainerBuilder<TModuleBase>
        where TModuleBase : IModule
    {
        IDependencyInjectionContainer Build(params TModuleBase[] modules);
    }
}
