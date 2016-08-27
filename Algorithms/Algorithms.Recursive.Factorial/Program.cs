using System;

namespace Algorithms.Recursive.Factorial
{
    class Program
    {
        static void Main(string[] args)
        {
            const int integerToGetFactorial = 6;
            Console.WriteLine("Finding the factorial of {0}", integerToGetFactorial);

            Factorial(integerToGetFactorial);

            Console.ReadLine();
        }

        private static int Factorial(int n)
        {
            if (n == 0)
            {
                Console.WriteLine(1);
                return 1;
            }

            //Console.WriteLine(n);
            var result = n*Factorial(n - 1);
            Console.WriteLine(result);
            return result;
        }
    }
}
