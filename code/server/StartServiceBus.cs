using common;

namespace server
{
    public class StartServiceBus : NeedStartup
    {
        public void run()
        {
            var receiver = Resolve.the<RhinoReceiver>();
            var handler = new MessageHandler(Resolve.the<DependencyRegistry>());
            receiver.register(x =>
            {
                handler.handler(x);
            });
            Resolve.the<CommandProcessor>().add(receiver);
        }
    }
}