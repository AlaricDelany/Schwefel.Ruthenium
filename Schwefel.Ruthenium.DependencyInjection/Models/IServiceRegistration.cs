using System;

namespace Schwefel.Ruthenium.DependencyInjection.Models
{
    public interface IServiceRegistration
    {
        Type BaseType { get; }
        Type ImplementationType { get; }

        //Func<object> Activator { get; } // TODO: Implement Activator
    }

    public abstract class ServiceRegistration<TBase> : IServiceRegistration
    {
        public Type BaseType => typeof(TBase);

        public abstract Type ImplementationType { get; }
    }

    public class TypeServiceRegistration<TBase, TImplementation> : ServiceRegistration<TBase>
        where TImplementation : TBase
    {
        public override Type ImplementationType => typeof(TImplementation);
    }

    public class TypeServiceRegistration : IServiceRegistration
    {
        public Type BaseType { get; set; }

        public Type ImplementationType { get; set; }
    }

    public interface IInstanceServiceRegistration : IServiceRegistration, IDisposable
    {
        object ImplementationObject { get; }
    }

    public interface IInstanceServiceRegistration<out TBase> : IInstanceServiceRegistration
    {
        Func<TBase> Implementation { get; }
    }

    public class InstanceRegistration<TBase> : ServiceRegistration<TBase>, IInstanceServiceRegistration<TBase>
    {
        public override Type ImplementationType => typeof(TBase);

        public void Dispose()
        {
            (Implementation as IDisposable)?.Dispose();
        }

        public Func<TBase> Implementation { get; private set; }

        public object ImplementationObject => Implementation;
    }

}
