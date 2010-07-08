namespace common
{
    public interface NeedStartup : Command {}

    public interface Command { void run();}
}