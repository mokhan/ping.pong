using System;
using System.IO;
using System.Reflection;

namespace common
{
    static public class Logging
    {
        static public Logger log<T>(this T item)
        {
            return new TextLogger(Console.Out);
        }

        static public void log(this string item, params object[] arguments)
        {
            new TextLogger(Console.Out).debug(item, arguments);
        }

        static public void add_to_log(this Exception item)
        {
            new TextLogger(Console.Out).debug(item.Message);
        }
    }

    public class TextLogger : Logger
    {
        readonly TextWriter writer;

        public TextLogger(TextWriter writer)
        {
            this.writer = writer;
        }

        public void debug(string message, params object[] arguments)
        {
            writer.WriteLine("{0}: {1}".format(Assembly.GetEntryAssembly().GetName().Name, message.format(arguments)));
        }
    }

    public interface Logger
    {
        void debug(string message, params object[] arguments);
    }
}