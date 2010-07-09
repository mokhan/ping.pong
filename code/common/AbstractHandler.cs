using System;

namespace common
{
    public abstract class AbstractHandler<T> : Handler<T>, Handler
    {
        bool can_handle(Type type)
        {
            return typeof (T).Equals(type);
        }

        public void handle(object item)
        {
            if (can_handle(item.GetType()))
            {
                handle((T) item);
            }
        }

        public abstract void handle(T item);
    }
}