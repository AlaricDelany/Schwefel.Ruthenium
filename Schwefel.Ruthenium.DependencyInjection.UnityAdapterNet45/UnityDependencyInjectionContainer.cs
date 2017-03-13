using Microsoft.Practices.Unity;
using System;

namespace Schwefel.Ruthenium.DependencyInjection.UnityAdapterNet45
{
    public class UnityDependencyInjectionContainer : IDependencyInjectionContainer, ISetUpAbleDependencyInjectionContainer
    {
        private readonly IUnityContainer _container = null;

        public UnityDependencyInjectionContainer(IUnityContainer container)
        {
            _container = container;
        }

        public void Dispose()
        {
            _container?.Dispose();
        }

        public IDependencyInjectionContainer GetTransactionalContainer()
        {
            return new UnityDependencyInjectionContainer(_container.CreateChildContainer());
        }

        public bool IsRegistered<T>()
        {
            return _container.IsRegistered<T>();
        }

        public bool IsRegistered(Type t)
        {
            return _container.IsRegistered(t);
        }

        public void RegisterInstance<TType>(TType instance)
        {
            _container.RegisterInstance(instance);
        }

        public void RegisterType<TInterface, TClass>() where TClass : TInterface
        {
            _container.RegisterType<TInterface, TClass>();
        }

        public void RegisterType<TInterface>(Type classType)
        {
            _container.RegisterType(typeof(TInterface), classType);
        }

        public void RegisterType(Type interfaceType, Type classType)
        {
            _container.RegisterType(interfaceType, classType);
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public object Resolve(Type t)
        {
            return _container.Resolve(t);
        }
    }
}
