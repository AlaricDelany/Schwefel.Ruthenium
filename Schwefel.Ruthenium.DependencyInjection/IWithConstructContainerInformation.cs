using System;

namespace Schwefel.Ruthenium.DependencyInjection
{
    public delegate void OnContructContainer(IDependencyInjectionContainer container);

    public interface IWithConstructContainerInformation : IDisposable
    {
        OnContructContainer OnConstructing { get; }
    }
}
