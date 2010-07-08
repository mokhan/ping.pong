using System;
using System.Collections.Generic;
using Autofac;

namespace common
{
    public class AutofacDependencyRegistry : DependencyRegistry
    {
        readonly Func<IContainer> container;

        public AutofacDependencyRegistry(Func<IContainer> container)
        {
            this.container = container;
        }

        public Interface get_a<Interface>()
        {
            return container().Resolve<Interface>();
        }

        public IEnumerable<Interface> get_all<Interface>()
        {
            return container().Resolve<IEnumerable<Interface>>();
        }
    }
}