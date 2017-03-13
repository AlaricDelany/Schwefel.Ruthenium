using Schwefel.Ruthenium.DependencyInjection.Modules;

namespace Schwefel.Ruthenium.DependencyInjection.UnityAdapterNet45.Modules
{
    public interface IUnityModule : IModule
    {
        void Configure(UnityDependencyInjectionContainer container);
    }
}
