using System;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Windows.Media.Media3D;
using LogicLib;
using OSLab_1.Extensions;

namespace Task5_Logic
{
    class Program
    {
        private static MainLogic _logicLib = new MainLogic();
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                using (PipeStream pipeClient =
                    new AnonymousPipeClientStream(PipeDirection.In, args[0]))
                {
                    using (StreamReader sr = new StreamReader(pipeClient))
                    {
                        // Display the read text to the console
                        string temp;

                        // Wait for 'sync message' from the server.
                        do
                        {
                            Thread.Sleep(2000);
                            Console.WriteLine("[CLIENT] Wait for message...");
                            temp = sr.ReadLine();

                            if (!string.IsNullOrEmpty(temp))
                            {
                                ChooseFunctionToExecute(temp);
                            }
                        }
                        while (temp == null || !temp.StartsWith("STOP"));
                        // Read the server data and echo to the console.
                        while ((temp = sr.ReadLine()) != null)
                        {
                            Console.WriteLine("[CLIENT] Echo: " + temp);
                           
                        }
                     
                    }
                }
            }
            Console.Write("[CLIENT] Press Enter to continue...");
            Console.ReadLine();
          
          Console.ReadKey();
        }

        private static void ChooseFunctionToExecute(string msg)
        {
            switch (msg)
            {
                case "Method1": ClickEventsOn_MultiplyTwoFigures(); break;
                case "Method2": ClickEventsOn_RemoveDuplicatesFromArray(); break;
                case "Method3": ClickEventsOn_MostFrequentWordInfile(); break;
                case "Method4": ClickEventsOn_SquareMatrix(); break;
                case "Method5": ClickEventsOn_PrimeNumbers();break;
            }
        }
        
        
        private static void ClickEventsOn_PrimeNumbers()
        {
            const uint number = 12;
            ConsoleEx.WriteColorfulText($"Результат разложения числа {number} на простые числа ч/з решето Эратосфена:"
                ,ConsoleColor.Yellow);
            var res = _logicLib.SieveEratosthenes(number);
            foreach (var primeNumber in res)
            {
                Console.Write(primeNumber+" ");
            }
            Console.WriteLine();
        }

        private static void ClickEventsOn_SquareMatrix()
        {
            var matrix = new[,]
            {
                {1, 2, 3},
                {4, 5, 6},
                {7, 8, 9}
            };
            ConsoleEx.WriteColorfulText("Исходная матрица:",ConsoleColor.Yellow);
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i,j] + " ");
                }

                Console.WriteLine();
            }
            var res = _logicLib.GetMaximumsMatrix(matrix);
            ConsoleEx.WriteColorfulText("Минимальный элемент среди элементов, расположенных ниже главной диагонали:"
                ,ConsoleColor.Yellow);
            Console.WriteLine(res.Item1);
           
            ConsoleEx.WriteColorfulText("Максимальный элемент, среди элементов расположенных выше побочной диагонали:",
                ConsoleColor.Yellow);
            Console.WriteLine(res.Item2);
            
        }

        private static void ClickEventsOn_MostFrequentWordInfile()
        {
            var res = _logicLib.GetMostFrequentWord(@"C:\Users\Sova IS\RiderProjects\OSLab_1\Task5_Logic\WordsFile.txt");
            ConsoleEx.WriteColorfulText("Исходный текст:",ConsoleColor.Yellow);
            Console.WriteLine(File.ReadAllText(@"C:\Users\Sova IS\RiderProjects\OSLab_1\Task5_Logic\WordsFile.txt"));
            ConsoleEx.WriteColorfulText("Самое часто встречаемающееся слово в файле :WordsFile.txt",ConsoleColor.Yellow);
            Console.WriteLine(res);
        }

        private static void ClickEventsOn_RemoveDuplicatesFromArray()
        {
            var res = _logicLib.GetUniqueValues(
                @"C:\Users\Sova IS\RiderProjects\OSLab_1\Task5_Logic\FileFloatArray.txt",
                @"C:\Users\Sova IS\RiderProjects\OSLab_1\Task5_Logic\FileOutputDistinctArray.txt");

            ConsoleEx.WriteColorfulText("Исходный массив",ConsoleColor.Yellow);
            Console.WriteLine(File.ReadAllText(@"C:\Users\Sova IS\RiderProjects\OSLab_1\Task5_Logic\FileFloatArray.txt"));
            
            ConsoleEx.WriteColorfulText("Уникальные значение массива,которые  были записаны в файл FileOutputDistinctArray.txt",
                ConsoleColor.Yellow);
            foreach (var d in res)
            {
                Console.Write(d+" ");
            }
            Console.WriteLine();
        }

        private static void ClickEventsOn_MultiplyTwoFigures()
        {
            var a = BigInteger.Parse(
                "100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            var b = BigInteger.Parse("2");
            var res = _logicLib.MultiplyBigIntegers(a, b);
            ConsoleEx.WriteColorfulText("Результат умножения двух больших чисел",ConsoleColor.Yellow);
            ConsoleEx.WriteColorfulText($"{a}*{b} =",ConsoleColor.Yellow);
            Console.WriteLine(res);
        }
    }
}