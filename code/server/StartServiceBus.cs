using common;
using common.messages;

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
                //using (var unit_of_work = Resolve.the<IUnitOfWorkFactory>().create())
                //{
                    handler.handler(x);
                    //unit_of_work.commit();
                //}
            });
            Resolve.the<CommandProcessor>().add(receiver);
            //ThreadPool.QueueUserWorkItem(x => receiver.run());
            Resolve.the<ServiceBus>().publish<StartedApplication>(x => x.message = "server");
        }
    }
}