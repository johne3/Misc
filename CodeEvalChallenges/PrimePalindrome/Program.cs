using System;
using System.Globalization;

namespace PrimePalindrome
{
    class Program
    {
        private static void Main(string[] args)
        {
            int greatestPalindrome = 0;
            for (int i = 0; i < 1000; i++)
            {
                if (IsPalindrome(i) && IsPrime(i) && i > greatestPalindrome)
                {
                    greatestPalindrome = i;
                }
            }

            Console.WriteLine(greatestPalindrome);
            Console.ReadLine();
        }

        private static bool IsPalindrome(int i)
        {
            var s = i.ToString(CultureInfo.InvariantCulture);
            return string.Equals(s, ReverseString(s));
        }

        private static string ReverseString(string s)
        {
            char[] chars = s.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        private static bool IsPrime(int number)
        {
            if (number < 3) return false;

            double boundary = Math.Floor(Math.Sqrt(number));
            for (int i = 2; i <= boundary; i++)
            {
                if (number%i == 0) return false;
            }

            return true;
        }
    }
}
