﻿using System;
using common;
using common.messages;

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
                handler.handler(x);
            });
            Resolve.the<CommandProcessor>().add(receiver);
            "sending ping {0}".log(DateTime.Now);
            Resolve.the<ServiceBus>().publish<Message>(x =>
            {
                x.source = "client";
                x.message = "ping";
            });
        }
    }
}