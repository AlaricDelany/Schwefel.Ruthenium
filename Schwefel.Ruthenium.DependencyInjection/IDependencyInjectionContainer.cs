using Schwefel.Ruthenium.DependencyInjection.Models;
using System;
using System.Collections.Generic;

namespace Schwefel.Ruthenium.DependencyInjection
{
    public interface IDependencyInjectionContainer : IDisposable
    {
        IDependencyInjectionContainer GetTransactionalContainer();

        bool IsRegistered<T>();
        bool IsRegistered(Type t);

        T Resolve<T>();
        object Resolve(Type t);

        IEnumerable<IServiceRegistration> ToArray();
    }
}