using System;
using OSLab_1;
using OSLab_1.Extensions;

namespace OS_Lab2.main
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleEx.WriteColorfulText("Сова Игорь КМБ Лаба № 2",ConsoleColor.Cyan);
            Console.WriteLine();
            
            var matrix = new int[3, 3];
            var matrixAsync = new MatrixAsync();
            var matrixClass = new Matrix();
            matrixAsync.FillRowRandomNumbers(ref matrix);
            matrixClass.Print(matrix);

            var res = matrixAsync.CountRowSum(matrix);
            ConsoleEx.WriteColorfulText($"Результат суммы строк матрицы: {res}",ConsoleColor.Magenta);
            Console.Read();
        }
    }
}