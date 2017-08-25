using System;

namespace Schwefel.Ruthenium.DependencyInjection
{
    public interface ISetUpAbleDependencyInjectionContainer
    {
        void RegisterType<TInterface, TClass>()
            where TClass : TInterface;

        void RegisterType<TInterface>(Type classType);
        void RegisterType(Type interfaceType, Type classType);

        void RegisterInstance<TType>(TType instance);
    }
}