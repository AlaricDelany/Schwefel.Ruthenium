using Microsoft.Practices.Unity;
using Schwefel.Ruthenium.DependencyInjection.UnityAdapterNet45.Modules;
using System.Linq;

namespace Schwefel.Ruthenium.DependencyInjection.UnityAdapterNet45
{
    public class UnityDependencyInjectionContainerBuilder : IDependencyInjectionContainerBuilder<IUnityModule>
    {
        public IDependencyInjectionContainer Build(params IUnityModule[] modules)
        {
            UnityDependencyInjectionContainer container = new UnityDependencyInjectionContainer(new UnityContainer());

            foreach (IUnityModule currentModule in modules.Where(m => m != null))
            {
                currentModule.Configure(container);
            }
            return container;
        }
    }
}
