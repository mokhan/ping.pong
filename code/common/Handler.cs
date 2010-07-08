namespace common
{
    public interface Handler
    {
        void handle(object item);
    }

    public interface Handler<T>
    {
        void handle(T item);
    }
}