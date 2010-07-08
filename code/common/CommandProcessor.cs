namespace common
{
    public interface CommandProcessor : Command
    {
        void add(Command command);
        void stop();
    }
}