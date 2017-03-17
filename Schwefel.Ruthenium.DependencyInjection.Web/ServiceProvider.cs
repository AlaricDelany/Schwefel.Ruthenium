using Microsoft.Extensions.DependencyInjection;
using System;

namespace Schwefel.Ruthenium.DependencyInjection.Web
{
    public class ServiceProvider<TDependencyContainer> : IServiceProvider, IDisposable
        where TDependencyContainer : IDependencyInjectionContainer
    {
        private IDependencyInjectionContainer _container;

        public ServiceProvider(IDependencyInjectionContainer container)
        {
            _container = container;
        }

        public void Dispose()
        {
            _container?.Dispose();
            _container = null;
        }

        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }
    }
}
