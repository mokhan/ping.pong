namespace common
{
    public interface CommandProcessor : Command
    {
        void add(Command command_to_process);
        void stop();
    }
}