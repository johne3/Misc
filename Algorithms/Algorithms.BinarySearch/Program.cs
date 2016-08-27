using System;
using System.Collections.Generic;

namespace Algorithms.BinarySearch
{
    class Program
    {
        static void Main(string[] args)
        {
            const int searchFor = 19;

            Console.WriteLine("Searching for the location of {0} in a list of numbers...", searchFor);
            Console.WriteLine();


            //Gets our list of numbers
            var numbers = GetNumbers();

            //Executes the binary search
            var loc = BinarySearch(numbers, searchFor);

            Console.WriteLine("Found {0} at index {1} in the list of numbers", searchFor, loc);
            

            Console.WriteLine();
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static IList<int> GetNumbers()
        {
            var numbers = new List<int>();

            for (int i = 1; i <= 22; i++)
            {
                numbers.Add(i);
            }

            return numbers;
        } 

        private static int BinarySearch(IList<int> numbers, int x)
        {
            int i = 0;
            int r = numbers.Count - 1;

            int location = 0;

            while (i < r)
            {
                int m = Convert.ToInt32(Math.Floor((double)(i + r)/2));

                if (x > numbers[m])
                {
                    i = m + 1;
                }
                else
                {
                    r = m;
                }
            }

            if (x == numbers[i])
            {
                location = Convert.ToInt32(i);
            }
            else
            {
                location = 0;
            }

            return location;
        }
    }
}
