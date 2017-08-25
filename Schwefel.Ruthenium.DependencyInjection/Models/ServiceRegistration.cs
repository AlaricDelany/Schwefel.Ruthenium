using System;

namespace Schwefel.Ruthenium.DependencyInjection.Models
{
    public class ServiceRegistration<TBase, TImplementation> : IServiceRegistration<TBase>
    {
        public ServiceRegistration(Func<TBase> activatorFunc)
            : this(activatorFunc, typeof(TImplementation), typeof(TBase))
        {
        }

        public ServiceRegistration(Func<TBase> activatorFunc, Type implementationType, Type baseType)
        {
            ActivatorFunc = activatorFunc;
            ImplementationType = implementationType;
            BaseType = baseType;
        }

        public Func<TBase> ActivatorFunc { get; }

        public Type BaseType { get; }

        public Type ImplementationType { get; }
    }


    public class ServiceRegistration<TBase> : ServiceRegistration<TBase, TBase>
    {
        public ServiceRegistration(Func<TBase> activatorFunc, Type implementationType)
            : this(activatorFunc, implementationType, typeof(TBase))
        {
        }

        public ServiceRegistration(Func<TBase> activatorFunc, Type implementationType, Type baseType)
            : base(activatorFunc, implementationType, baseType)
        {
        }
    }
}