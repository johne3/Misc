using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.InsertionSort
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bubble Sort");

            var numbers = GetNumbersToSort();
            var str = "";
            foreach (var number in numbers)
            {
                str = string.Format("{0} {1}", str, number);
            }
            Console.WriteLine("Pre-sorted: {0}", str);
            Console.WriteLine();

            var sortedNumbers = InsertionSort(numbers.ToList());
            str = "";
            foreach (var sortedNumber in sortedNumbers)
            {
                str = string.Format("{0} {1}", str, sortedNumber);
            }
            Console.WriteLine("Post-sorted {0}", str);

            Console.WriteLine();
            Console.ReadLine();
        }

        private static List<int> GetNumbersToSort()
        {
            var numbers = new List<int>();
            var random = new Random();

            for (int i = 0; i < 5; i++)
            {
                numbers.Add(random.Next(1, 9));
            }

            return numbers;
        }

        private static List<int> InsertionSort(List<int> numbersToSort)
        {




            return numbersToSort;
        } 
    }
}
