namespace common
{
    static public class Resolve
    {
        static DependencyRegistry underlying_registry;

        static public void initialize_with(DependencyRegistry registry)
        {
            underlying_registry = registry;
        }

        static public DependencyToResolve the<DependencyToResolve>()
        {
            return underlying_registry.get_a<DependencyToResolve>();
        }

        static public bool is_initialized()
        {
            return underlying_registry != null;
        }
    }
}