using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using Autofac.Builder;
using common;
using Rhino.Queues;

namespace client
{
    class Client
    {
        static void Main()
        {
            Process.Start(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\server\bin\Debug\server.exe")));

            try
            {
                AppDomain.CurrentDomain.UnhandledException += (o, e) =>
                {
                    (e.ExceptionObject as Exception).add_to_log();
                };
                AppDomain.CurrentDomain.ProcessExit += (o, e) =>
                {
                    "shutting down".log();
                    try
                    {
                        Resolve.the<CommandProcessor>().stop();
                        Resolve.the<IQueueManager>().Dispose();
                    }
                    catch {}
                    Environment.Exit(Environment.ExitCode);
                };
                run();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                e.add_to_log();
                Console.Out.WriteLine(e);
                Console.ReadLine();
            }
        }

        static void run()
        {
            var builder = new ContainerBuilder();
            var registry = new AutofacDependencyRegistryBuilder(builder).build();
            Resolve.initialize_with(registry);
            builder.Register(x => registry).As<DependencyRegistry>().SingletonScoped();

            //needs startups
            builder.Register<StartServiceBus>().As<NeedStartup>();

            // infrastructure

            var manager = new QueueManager(new IPEndPoint(IPAddress.Loopback, 2201), "client.esent");
            manager.CreateQueues("client");
            builder.Register(x => new RhinoPublisher("server", 2200, manager)).As<ServiceBus>().SingletonScoped();
            builder.Register(x => new RhinoReceiver(manager.GetQueue("client"), x.Resolve<CommandProcessor>())).As<RhinoReceiver>().As<Receiver>().SingletonScoped();


            // commanding
            //builder.Register<AsynchronousCommandProcessor>().As<CommandProcessor>().SingletonScoped();
            builder.Register<SynchronousCommandProcessor>().As<CommandProcessor>().SingletonScoped();

            builder.Register<RequestHandler>().As<Handler>();

            Resolve.the<IEnumerable<NeedStartup>>().each(x => x.run());
            Resolve.the<CommandProcessor>().run();
            "started".log();
        }
    }
}