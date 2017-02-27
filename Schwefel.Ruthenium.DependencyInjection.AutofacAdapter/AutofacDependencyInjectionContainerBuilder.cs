using Autofac;
using System;
using System.Linq;

namespace Schwefel.Ruthenium.DependencyInjection.AutofacAdapter
{
    public class AutofacDependencyInjectionContainerBuilder : IDependencyInjectionContainerBuilder
    {
        private ContainerBuilder _ContainerBuilder;
        private OnContructContainer _OnConstructing;

        public AutofacDependencyInjectionContainerBuilder()
        {
            _ContainerBuilder = new ContainerBuilder();
        }

        public event OnContructContainer OnConstructing
        {
            add
            {
                _OnConstructing += value;
            }
            remove
            {
                _OnConstructing -= value;
            }
        }

        public void Dispose()
        {
            var listner = _OnConstructing.GetInvocationList().ToArray();

            foreach(var constructionInstruction in listner?.Where(d => d != null).OfType<OnContructContainer>())
            {
                OnConstructing -= constructionInstruction;
            }
        }

        public IDependencyInjectionContainer Build()
        {
            var container = _ContainerBuilder.Build();
            var autofacDependencyInjectionContainer = new AutofacDependencyInjectionContainer(container);

            _OnConstructing?.Invoke(autofacDependencyInjectionContainer);

            return autofacDependencyInjectionContainer;
        }
    }
}
