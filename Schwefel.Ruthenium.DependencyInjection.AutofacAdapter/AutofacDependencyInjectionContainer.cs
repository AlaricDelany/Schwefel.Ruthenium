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

        public void RegisterInstance<TInterface>(TInterface instance)
        {
            throw new NotImplementedException();
        }

        public void RegisterType<TInterface, TImplementation>()
        {
            throw new NotImplementedException();
        }

        public T Resolve<T>()
        {
            return _LifetimeScope.Resolve<T>();
        }

        public object Resolve(Type t)
        {
            return _LifetimeScope.Resolve(t);
        }
    }
}