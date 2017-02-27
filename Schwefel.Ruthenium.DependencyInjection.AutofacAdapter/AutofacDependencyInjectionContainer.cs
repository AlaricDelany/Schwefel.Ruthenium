using System;
using Autofac;

namespace Schwefel.Ruthenium.DependencyInjection.AutofacAdapter
{
    public class AutofacDependencyInjectionContainer : IDependencyInjectionContainer
    {
        private ILifetimeScope _LifetimeScope = null;

        public AutofacDependencyInjectionContainer(ILifetimeScope lifetimeScope) 
            : base()
        {
            _LifetimeScope = lifetimeScope;
        }

        public void Dispose()
        {
            _LifetimeScope?.Dispose();
        }
    }
}
