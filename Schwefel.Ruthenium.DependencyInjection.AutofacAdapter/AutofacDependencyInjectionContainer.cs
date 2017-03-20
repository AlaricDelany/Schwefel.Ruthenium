using System;
using System.Collections.Generic;
using Autofac;
using Schwefel.Ruthenium.DependencyInjection.Models;

namespace Schwefel.Ruthenium.DependencyInjection.AutofacAdapter
{
    public class AutofacDependencyInjectionContainer : IDependencyInjectionContainer
    {
        private readonly ILifetimeScope _LifetimeScope = null;

        public AutofacDependencyInjectionContainer(ILifetimeScope lifetimeScope)
        {
            _LifetimeScope = lifetimeScope;
        }

        public void Dispose()
        {
            _LifetimeScope?.Dispose();
        }

        public IDependencyInjectionContainer GetTransactionalContainer()
        {
            ILifetimeScope lifetimeScope = _LifetimeScope.BeginLifetimeScope();

            return new AutofacDependencyInjectionContainer(lifetimeScope);
        }

        public bool IsRegistered<T>()
        {
            return _LifetimeScope.IsRegistered<T>();
        }

        public bool IsRegistered(Type t)
        {
            return _LifetimeScope.IsRegistered(t);
        }

        public T Resolve<T>()
        {
            return _LifetimeScope.Resolve<T>();
        }

        public object Resolve(Type t)
        {
            return _LifetimeScope.Resolve(t);
        }

        /// <inheritdoc />
        public IEnumerable<IServiceRegistration> ToArray()
        {
            throw  new NotImplementedException();

            /*return _LifetimeScope.ComponentRegistry.Registrations.Select(r =>
            {
                IServiceRegistration lResult = null;


                


                return lResult;
            });*/
        }
    }
}