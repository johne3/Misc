using System;
using System.IO;

namespace ReverseWords
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (StreamReader reader = File.OpenText(args[0]))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] words = line.Split(' ');
                    Array.Reverse(words);

                    string output = string.Empty;
                    foreach (var word in words)
                    {
                        output = string.Format("{0} {1}", output, word);
                    }

                    Console.WriteLine(output.Trim());
                    
                }
            }
            Console.ReadLine();
        }
    }
}
