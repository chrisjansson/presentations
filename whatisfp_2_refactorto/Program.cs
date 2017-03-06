using System;
using Utils;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var r1 = Maybe<string>.Some("Hii")
                .DefaultIfNone("default value");

            var r2 = Maybe<string>.None
                .DefaultIfNone("default value");

            Console.WriteLine(r1);
            Console.WriteLine(r2);
        }
    }
}