using System;

namespace Schwefel.Ruthenium.DependencyInjection
{
    public interface IDependencyInjectionContainer: IDisposable
    {
        IDependencyInjectionContainer GetTransactionalContainer();

        bool IsRegistered<T>();
        bool IsRegistered(Type t);

        T Resolve<T>();
        object Resolve(Type t);


        void RegisterInstance<TInterface>(TInterface instance);
        void RegisterType<TInterface, TImplementation>();


    }
}
