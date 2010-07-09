using System;
using System.Reflection;
using System.Threading;
using common.messages;

namespace common
{
    public class RequestHandler : AbstractHandler<Message>
    {
        ServiceBus bus;

        public RequestHandler(ServiceBus bus)
        {
            this.bus = bus;
        }

        public override void handle(Message item)
        {
            "received {0} from {1} {2}".log(item.message, item.source, DateTime.Now);
            Thread.Sleep(5000);
            var source = Assembly.GetEntryAssembly().GetName().Name;
            "sending  {0} from {1} {2}".log(item.message.Equals("ping") ? "pong" : "ping", source, DateTime.Now);
            bus.publish<Message>(x =>
            {
                x.message = item.message.Equals("ping") ? "pong" : "ping";
                x.source = source;
            });
        }
    }
}