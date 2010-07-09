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
            "received from {0}: {1} {2}".log(item.source, item.message, DateTime.Now);
            Thread.Sleep(5000);
            bus.publish<Message>(x =>
            {
                x.message = item.message.Equals("ping") ? "pong" : "ping";
                x.source = Assembly.GetEntryAssembly().GetName().Name;
            });
        }
    }
}