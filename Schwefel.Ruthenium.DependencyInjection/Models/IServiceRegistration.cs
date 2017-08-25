using System;

namespace Schwefel.Ruthenium.DependencyInjection.Models
{
    public interface IServiceRegistration<out TBase> : IServiceRegistration
    {
        Func<TBase> ActivatorFunc { get; }
    }



    public interface IServiceRegistration
    {
        Type BaseType { get; }
        Type ImplementationType { get; }
    }
}
