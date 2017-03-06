using System;
using Utils;
using static Utils.Maybe;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var r1 = Some("Hii")
                .DefaultIfNone("default value");

            var r2 = None<string>()
                .DefaultIfNone("default value");

            Console.WriteLine(r1);
            Console.WriteLine(r2);
        }
    }
}