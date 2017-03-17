using Schwefel.Ruthenium.DependencyInjection.Web.Modules;
using Autofac.Extensions.DependencyInjection;

namespace Schwefel.Ruthenium.DependencyInjection.AutofacAdapter
{
    public static class AutofacDependencyInjectionContainerBuilderExtentions
    {
        public static void Populate(this IAutofacDependencyInjectionContainerBuilder self, IWebModule webModule)
        {
            self.Builder.Populate(webModule);
        }
    }
}
