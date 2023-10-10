#define CONDITION12
#define CONDITION2

using Attribute;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

[Author1("", Version = 1.0)]
class Test
{
    static void Main()
    {
        Console.WriteLine("Calling Method1");
        Method1(3);
        Console.WriteLine("Calling Method2");
        Method2();

        Console.WriteLine("Using the Debug class");
        Debug.Listeners.Add(new ConsoleTraceListener());
        Debug.WriteLine("DEBUG is defined");

        UnitTest();
        var x = 1;

#line 200 "Special"
        int i;
        int j;
#line default
        char c;
        float f;
#line hidden // numbering not affected
        string s;
        double d;

#if NET4811
        Console.WriteLine("NET48");
#endif

#if (DEBUG && !MYTEST)
        Console.WriteLine("DEBUG is defined");

#elif (!DEBUG && MYTEST)
        Console.WriteLine("MYTEST is defined");
#elif (DEBUG && MYTEST)
        Console.WriteLine("DEBUG and MYTEST are defined");  
#else
        Console.WriteLine("DEBUG and MYTEST are not defined");
#endif
    }
    [Conditional("RELEASE")]
    public static void UnitTest()
    {
        // code to do unit testing...
        Console.WriteLine("DEBUG");
    }
    [Conditional("CONDITION1")]
    [Obsolete("dsffds")]
    public static void Method1(int x)
    {
        Console.WriteLine("CONDITION1 is defined");
    }

    [Conditional("CONDITION1"), Conditional("CONDITION2")]
    public static void Method2()
    {
        Console.WriteLine("CONDITION1 or CONDITION2 is defined");
    }
}

/*
When compiled as shown, the application (named ConsoleApp)
produces the following output.

Calling Method1
CONDITION1 is defined
Calling Method2
CONDITION1 or CONDITION2 is defined
Using the Debug class
DEBUG is defined
*/