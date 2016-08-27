using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Recursive.Exponent
{
    class Program
    {
        static void Main(string[] args)
        {
            const int number = 25;
            const int exponent = 4;

            RecursiveExponent(number, exponent);

            Console.ReadLine();
        }

        private static int RecursiveExponent(int number, int exponent)
        {
            if (exponent == 0) return 1;

            var result = number*RecursiveExponent(number, exponent - 1);
            Console.WriteLine(result);
            return result;
        }
    }
}
