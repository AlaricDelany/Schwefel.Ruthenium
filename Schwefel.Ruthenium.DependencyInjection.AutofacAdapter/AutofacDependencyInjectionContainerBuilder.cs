using Autofac;
using Schwefel.Ruthenium.DependencyInjection.AutofacAdapter.Modules;
using System.Linq;

namespace Schwefel.Ruthenium.DependencyInjection.AutofacAdapter
{
    public interface IAutofacDependencyInjectionContainerBuilder : IDependencyInjectionContainerBuilder<IAutofacModule>
    {
        ContainerBuilder Builder { get; }
    }

    public class AutofacDependencyInjectionContainerBuilder : IAutofacDependencyInjectionContainerBuilder
    {
        public ContainerBuilder Builder { get; private set; }

        public AutofacDependencyInjectionContainerBuilder()
        {
            Builder = new ContainerBuilder();
        }

        public IDependencyInjectionContainer Build(params IAutofacModule[] modules)
        {
            foreach(var module in modules?.Where(m => m != null))
            {
                Builder.RegisterModule(module);
            }
            var container = Builder.Build();
            var autofacDependencyInjectionContainer = new AutofacDependencyInjectionContainer(container);

            return autofacDependencyInjectionContainer;

        }
    }
}
