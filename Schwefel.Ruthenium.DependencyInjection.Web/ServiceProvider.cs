using Microsoft.Extensions.DependencyInjection;
using System;

namespace Schwefel.Ruthenium.DependencyInjection.Web
{
    public class ServiceProvider<TDependencyContainer> : IServiceProvider, IDisposable
        where TDependencyContainer : class, IDependencyInjectionContainer
    {
        private TDependencyContainer _container;

        public ServiceProvider(TDependencyContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public void Dispose()
        {
            _container?.Dispose();
            _container = null;
        }

        public object GetService(Type serviceType)
        {
            if(_container == null)
                throw new ObjectDisposedException(nameof(_container));

            return _container.Resolve(serviceType);
        }
    }
}
