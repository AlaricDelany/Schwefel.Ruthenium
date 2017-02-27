using System;

namespace Schwefel.Ruthenium.DependencyInjection
{
    public delegate void OnContructContainer(IDependencyInjectionContainer container);

    public interface IWithConstructContainerInformation : IDisposable
    {
        event OnContructContainer OnConstructing;
    }
}
