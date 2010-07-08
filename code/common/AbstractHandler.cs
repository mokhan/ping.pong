using System;

namespace common
{
    public abstract class AbstractHandler<T> : Handler<T>, Handler
    {
        bool can_handle(Type type)
        {
            this.log().debug("{0} can handle {1} = {2}", this, type, typeof (T).Equals(type));
            return typeof (T).Equals(type);
        }

        public void handle(object item)
        {
            if (can_handle(item.GetType()))
            {
                this.log().debug("handling... {0}", item);
                handle((T) item);
            }
        }

        public abstract void handle(T item);
    }
}