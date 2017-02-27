using Autofac;
using Schwefel.Ruthenium.DependencyInjection.AutofacAdapter.Modules;
using System;
using System.Linq;

namespace Schwefel.Ruthenium.DependencyInjection.AutofacAdapter
{
    public class AutofacDependencyInjectionContainerBuilder : IDependencyInjectionContainerBuilder<IAutofacModule>
    {
        private ContainerBuilder _ContainerBuilder;

        public AutofacDependencyInjectionContainerBuilder()
        {
            _ContainerBuilder = new ContainerBuilder();
        }

        public IDependencyInjectionContainer Build(params IAutofacModule[] modules)
        {
            foreach(var module in modules?.Where(m => m != null))
            {
                _ContainerBuilder.RegisterModule(module);
            }
            var container = _ContainerBuilder.Build();
            var autofacDependencyInjectionContainer = new AutofacDependencyInjectionContainer(container);

            return autofacDependencyInjectionContainer;

        }
    }
}
