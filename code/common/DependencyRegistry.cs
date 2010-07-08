using System.Collections.Generic;

namespace common
{
    public interface DependencyRegistry
    {
        T get_a<T>();
        IEnumerable<T> get_all<T>();
    }
}