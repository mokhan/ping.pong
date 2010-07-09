using System;

namespace common
{
    static public class Logging
    {
        static public void log(this string item, params object[] arguments)
        {
            //Console.Out.WriteLine("{0}: {1}".format(Assembly.GetEntryAssembly().GetName().Name, item.format(arguments)));
            Console.Out.WriteLine(item.format(arguments));
        }

        static public void add_to_log(this Exception item)
        {
            item.Message.log();
        }
    }
}