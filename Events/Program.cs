using System;

namespace Delegates
{
    public delegate void DelEventHandler();

    class Program
    {
        public static event DelEventHandler add;

        static void Main(string[] args)
        {
            add += USA;
            add += India;
            add += England;
            add.Invoke();

            Console.ReadLine();
        }
        static void USA()
        {
            Console.WriteLine("USA");
        }

        static void India()
        {
            Console.WriteLine("India");
        }

        static void England()
        {
            Console.WriteLine("England");
        }
    }
}