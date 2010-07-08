using System;
using System.Collections.Generic;
using System.Net;
using Autofac.Builder;
using common;
using Rhino.Queues;

namespace server
{
    class Server
    {
        static void Main(string[] args)
        {
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

            var manager = new QueueManager(new IPEndPoint(IPAddress.Loopback, 2200), "server.esent");
            manager.CreateQueues("server");
            builder.Register(x => new RhinoPublisher("client", 2201, manager)).As<ServiceBus>().SingletonScoped();
            builder.Register(x => new RhinoReceiver(manager.GetQueue("server"), x.Resolve<CommandProcessor>())).As<RhinoReceiver>().As<Receiver>().SingletonScoped();

            // commanding
            builder.Register<AsynchronousCommandProcessor>().As<CommandProcessor>().SingletonScoped();
            builder.Register<StartedApplicationHandler>().As<Handler>();

            // queries

            // repositories
            //builder.Register<NHibernatePersonRepository>().As<PersonRepository>().FactoryScoped();
            //builder.Register<NHibernateAccountRepository>().As<AccountRepository>().FactoryScoped();


            Resolve.the<IEnumerable<NeedStartup>>().each(x => x.run());
            Resolve.the<CommandProcessor>().run();
        }
    }
}