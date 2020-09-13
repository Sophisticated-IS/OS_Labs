using System;

namespace OSLab_1
{
    public sealed class Program
    {
        /// <summary>
        /// Лабораторная работа по Защите ОС №1
        /// Автор: Сова Игорь КМБ
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var matrix = new Matrix();
            var randomMatrix = matrix.GetRandomMatrix(10,3);

            matrix.Print(randomMatrix);
            var masTEST = new int[] {2, 3, 6, 7, 8, 3, 0, 2, 0, 9, 6};
            matrix.CountingSort(ref masTEST,0,9,Matrix.SortOrder.Asc);
            Console.ReadLine();
        }
    }
}