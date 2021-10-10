namespace DownKyi.Core.Utils.Debug
{
    public static class Console
    {
        public static void PrintLine()
        {
#if DEBUG
            System.Console.WriteLine();
#endif
        }

        public static void PrintLine(float value)
        {
#if DEBUG
            System.Console.WriteLine(value);
#endif
        }

        public static void PrintLine(int value)
        {
#if DEBUG
            System.Console.WriteLine(value);
#endif
        }

        public static void PrintLine(uint value)
        {
#if DEBUG
            System.Console.WriteLine(value);
#endif
        }

        public static void PrintLine(long value)
        {
#if DEBUG
            System.Console.WriteLine(value);
#endif
        }

        public static void PrintLine(ulong value)
        {
#if DEBUG
            System.Console.WriteLine(value);
#endif
        }

        public static void PrintLine(object value)
        {
#if DEBUG
            System.Console.WriteLine(value);
#endif
        }

        public static void PrintLine(string value)
        {
#if DEBUG
            System.Console.WriteLine(value);
#endif
        }

        public static void PrintLine(string format, object arg0)
        {
#if DEBUG
            System.Console.WriteLine(format, arg0);
#endif
        }

        public static void PrintLine(string format, object arg0, object arg1)
        {
#if DEBUG
            System.Console.WriteLine(format, arg0, arg1);
#endif
        }

        public static void PrintLine(string format, object arg0, object arg1, object arg2)
        {
#if DEBUG
            System.Console.WriteLine(format, arg0, arg1, arg2);
#endif
        }

        public static void PrintLine(string format, object arg0, object arg1, object arg2, object arg3)
        {
#if DEBUG
            System.Console.WriteLine(format, arg0, arg1, arg2);
#endif
        }

        public static void PrintLine(string format, params object[] arg)
        {
#if DEBUG
            System.Console.WriteLine(format, arg);
#endif
        }

        public static void PrintLine(char[] buffer, int index, int count)
        {
#if DEBUG
            System.Console.WriteLine(buffer, index, count);
#endif
        }

        public static void PrintLine(decimal value)
        {
#if DEBUG
            System.Console.WriteLine(value);
#endif
        }

        public static void PrintLine(char[] buffer)
        {
#if DEBUG
            System.Console.WriteLine(buffer);
#endif
        }

        public static void PrintLine(char value)
        {
#if DEBUG
            System.Console.WriteLine(value);
#endif
        }

        public static void PrintLine(bool value)
        {
#if DEBUG
            System.Console.WriteLine(value);
#endif
        }

        public static void PrintLine(double value)
        {
#if DEBUG
            System.Console.WriteLine(value);
#endif
        }

    }
}
