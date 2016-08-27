using System;
using System.IO;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            using (StreamReader reader = File.OpenText(args[0]))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    //Do something with line
                    string outLine = string.Empty;
                    string[] splitLine = line.Split(' ');
                    int firstNumber = int.Parse(splitLine[0]);
                    int secondNumber = int.Parse(splitLine[1]);
                    int thirdNumber = int.Parse(splitLine[2]);

                    for (int i = 1; i <= thirdNumber; i++)
                    {
                        if ((i%firstNumber == 0) && (i%secondNumber == 0))
                        {
                            outLine = string.Format("{0} {1}", outLine, "FB");
                        }
                        else
                        {
                            if (i%firstNumber == 0)
                            {
                                outLine = string.Format("{0} {1}", outLine, "F");
                            }
                            else if (i%secondNumber == 0)
                            {
                                outLine = string.Format("{0} {1}", outLine, "B");
                            }
                            else
                            {
                                outLine = string.Format("{0} {1}", outLine, i);
                            }
                        }
                    }

                    Console.WriteLine(outLine);
                }
            }
        }
    }
}
