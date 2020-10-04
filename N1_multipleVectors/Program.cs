using System;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using OSLab_1.Extensions;

namespace N1_multipleVectors
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleEx.WriteColorfulText("Введите первый вектор ч/з пробел:",ConsoleColor.Yellow);
            var inpV1 = Console.ReadLine();
            var values1 = inpV1.Split(' ').Select(double.Parse).ToArray();
            ConsoleEx.WriteColorfulText("Введите второй вектор ч/з пробел:",ConsoleColor.Yellow);
            var inpV2 = Console.ReadLine();
            var values2 = inpV2.Split(' ').Select(double.Parse).ToArray();

            var vector1 = Vector<double>.Build.SparseOfArray(values1);
            var vector2 = Vector<double>.Build.SparseOfArray(values2);
            
            Console.WriteLine(vector1*vector2);

            Console.ReadLine();
        }
    }
}