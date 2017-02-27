using System;

namespace Schwefel.Ruthenium.DependencyInjection
{
    public interface IDependencyInjectionContainerBuilder : IWithConstructContainerInformation
    {
        IDependencyInjectionContainer Build();
    }
}
