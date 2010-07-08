using common;

namespace client
{
    public class StartServiceBus : NeedStartup
    {
        public void run()
        {
            var receiver = Resolve.the<RhinoReceiver>();
            var handler = new MessageHandler(Resolve.the<DependencyRegistry>());
            receiver.register(x =>
            {
                // synchronize with ui thread?
                handler.handler(x);
            });
            Resolve.the<CommandProcessor>().add(receiver);
            //Resolve.the<ServiceBus>().publish<StartedApplication>(x => x.message = "client");
        }
    }
}