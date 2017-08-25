using System;

namespace Schwefel.Ruthenium.DependencyInjection.Models
{
    public class InstanceServiceRegistration<TBase> : ServiceRegistration<TBase>, IDisposable
    {
        public IDisposable InstnaceDisposable { get; }


        public InstanceServiceRegistration(TBase instance)
            : base(() => instance, (Type) instance.GetType())
        {
            if (instance is IDisposable)
                InstnaceDisposable = instance as IDisposable;
        }

        public void Dispose()
        {
            InstnaceDisposable?.Dispose();
        }
    }
}