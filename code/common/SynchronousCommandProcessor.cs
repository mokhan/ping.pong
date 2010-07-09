using System.Collections.Generic;

namespace common
{
    public class SynchronousCommandProcessor : CommandProcessor
    {
        readonly Queue<Command> queued_commands;

        public SynchronousCommandProcessor()
        {
            queued_commands = new Queue<Command>();
        }

        public void add(Command command_to_process)
        {
            queued_commands.Enqueue(command_to_process);
        }

        public void run()
        {
            while (queued_commands.Count > 0) queued_commands.Dequeue().run();
        }

        public void stop()
        {
            queued_commands.Clear();
        }
    }
}