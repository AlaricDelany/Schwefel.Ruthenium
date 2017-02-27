using Autofac;
using System;
using System.Linq;

namespace Schwefel.Ruthenium.DependencyInjection.AutofacAdapter
{
    public class AutofacDependencyInjectionContainerBuilder : IDependencyInjectionContainerBuilder
    {
        private ContainerBuilder _ContainerBuilder;

        public AutofacDependencyInjectionContainerBuilder()
        {
            _ContainerBuilder = new ContainerBuilder();
        }

        public OnContructContainer OnConstructing { get; private set; }

        public void Dispose()
        {
            var listner = OnConstructing.GetInvocationList().ToArray();

            foreach(var constructionInstruction in listner?.Where(d => d != null).OfType<OnContructContainer>())
            {
                OnConstructing -= constructionInstruction;
            }
        }

        public IDependencyInjectionContainer Build()
        {
            var container = _ContainerBuilder.Build();
            var autofacDependencyInjectionContainer = new AutofacDependencyInjectionContainer(container);

            OnConstructing?.Invoke(autofacDependencyInjectionContainer);

            return autofacDependencyInjectionContainer;
        }
    }
}
