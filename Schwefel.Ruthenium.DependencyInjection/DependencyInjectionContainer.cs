using System.Collections.Generic;
using System.Linq;

namespace Schwefel.Ruthenium.DependencyInjection
{
    public abstract class DependencyInjectionContainer<TConfigurationBuilder> : IDependencyInjectionContainer
        where TConfigurationBuilder : class, new()
    {
        public delegate void OnContructContainer(IDependencyInjectionContainer container, TConfigurationBuilder configBuilder);

        private readonly IEnumerable<OnContructContainer> _constructingInstructions;

        protected OnContructContainer OnConstructing { get; private set; }

        public DependencyInjectionContainer(params OnContructContainer[] constructingInstructions)
        {
            _constructingInstructions = constructingInstructions;

            Construct();
        }

        protected void Construct()
        {
            foreach(var constructingInstruction in _constructingInstructions?.Where(d => d != null))
            {
                OnConstructing += constructingInstruction;
            }
        }

        public virtual void Dispose()
        {
            foreach(var constructionInstruction in _constructingInstructions?.Where(d => d != null))
            {
                OnConstructing -= constructionInstruction;
            }
        }
    }
}
