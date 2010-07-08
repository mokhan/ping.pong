namespace common
{
    static public class StringFormatting
    {
        static public string format(this string message, params object[] arguments)
        {
            return string.Format(message, arguments);
        }
    }
}