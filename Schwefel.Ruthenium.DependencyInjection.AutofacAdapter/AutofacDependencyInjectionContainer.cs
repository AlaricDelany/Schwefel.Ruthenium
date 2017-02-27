using Autofac;

namespace Schwefel.Ruthenium.DependencyInjection.AutofacAdapter
{
    public class AutofacDependencyInjectionContainer : DependencyInjectionContainer<ContainerBuilder>, IDependencyInjectionContainer
    {
        public AutofacDependencyInjectionContainer(params OnContructContainer[] constructingInstructions) 
            : base(constructingInstructions)
        {

        }
    }
}
