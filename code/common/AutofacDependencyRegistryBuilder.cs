using System;
using Autofac;
using Autofac.Builder;
using Autofac.Modules;

namespace common
{
    public class AutofacDependencyRegistryBuilder
    {
        readonly Func<IContainer> container;

        public AutofacDependencyRegistryBuilder(ContainerBuilder builder)
        {
            builder.RegisterModule(new ImplicitCollectionSupportModule());
            builder.SetDefaultScope(InstanceScope.Factory);
            container = () => builder.Build();
            container = container.memorize();
        }

        public DependencyRegistry build()
        {
            return new AutofacDependencyRegistry(container);
        }
    }
}