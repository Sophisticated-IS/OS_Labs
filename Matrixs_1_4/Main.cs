using System;
using System.Linq;
using OSLab_1.Extensions;

namespace OSLab_1
{
    public sealed class Program
    {
        /// <summary>
        /// Лабораторная работа по Защите ОС №1-3
        /// Автор: Сова Игорь КМБ
        /// </summary>
        static void Main(string[] args)
        {
            var matrix = new Matrix();
            
            var intMass = new[,]
            {
                {1,2,4},
                {5,5,9},
                {0,2,1}
            };

            ConsoleEx.WriteColorfulText("Сортировка целых чисел методом подсчета по убыванию:",ConsoleColor.Yellow);
            matrix.Print(intMass);
            matrix.SortMatrixByRow(ref intMass,0,9,Matrix.SortOrder.Desc);
            matrix.Print(intMass);
            Console.WriteLine();

            ConsoleEx.WriteColorfulText( "Сортировка вещественных чисел методом подсчета по убыванию:",ConsoleColor.Yellow);
            var randomMatrix = matrix.GetRandomMatrix<double>(10,3);//Создание матрицы в многопотоке
            matrix.Print(randomMatrix);
            matrix.CountingSortDoubleByRow(ref randomMatrix);
            matrix.Print(randomMatrix);


            Console.WriteLine();
            ConsoleEx.WriteColorfulText( "Умножение матрицы на множитель:",ConsoleColor.Cyan);
            ConsoleEx.WriteColorfulText( "Введите множитель для матрицы:",ConsoleColor.Yellow);
            var coeff = Convert.ToInt32(Console.ReadLine());
            var matrixResult = matrix.GetRandomMatrix<int>(5, 7);
            matrix.Print(matrixResult);
            matrix.MultipleOnCoefficient(ref matrixResult,coeff);
            matrix.Print(matrixResult);
            Console.ReadLine();
        }
    }
}