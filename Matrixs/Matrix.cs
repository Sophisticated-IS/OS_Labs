using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace OSLab_1
{
    public sealed class Matrix
    {
        public enum SortOrder
        {
            Asc,
            Desc
        }

        /// <summary>
        /// Заполняет матрицу в многопоточном режиме (Задание 1)
        /// </summary>
        /// <param name="matrix"></param>
        public double[,] GetRandomMatrix(int firstDim, int secondDim)
        {
            var matrix = new double[firstDim, secondDim];
            var rand = new Random();
            Parallel.For(0, firstDim, i =>
                {
                    for (int j = 0; j < secondDim; j++)
                    {
                        matrix[i, j] = rand.NextDouble() + rand.Next(0, 100);
                    }
                }
            );

            return matrix;
        }

        public void Print(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write("{0:0.##}\t", matrix[i, j]);
                }

                Console.WriteLine();
            }
        }

        public void CountingSort(ref int[] matrix, int startInterval, int endInterval, SortOrder sortOrder)
        {
            if (endInterval < startInterval)
                throw new ArgumentOutOfRangeException(nameof(startInterval) + '➜' + nameof(endInterval));
            var dictValuesRange = new Dictionary<int, int>();
            var index = 0;
            for (int i = startInterval; i < endInterval + 1; i++)
            {
                dictValuesRange.Add(i, index);
                index++;
            }


            var counterMassive = new double[endInterval - startInterval + 1];
            for (int i = 0; i < matrix.Length; i++)
            {
                counterMassive[dictValuesRange[matrix[i]]]++;
            }

            switch (sortOrder)
            {
                case SortOrder.Asc:
                    for (int i = 0; i < counterMassive.Length; i++)
                    {
                        for (int j = 0; j < counterMassive[i]; j++)
                        {
                            Console.Write(dictValuesRange[i]);
                        }
                    }

                    break;
                case SortOrder.Desc:
                    for (int i = counterMassive.Length - 1; i >= 0; i--)
                    {
                        for (int j = 0; j < counterMassive[i]; j++)
                        {
                            Console.Write(dictValuesRange[i]);
                        }
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(sortOrder), sortOrder, null);
            }
            //TODO ВЫВОд 
            //TODO 2.	Дана матрица из вещественных чисел, содержащая n строк и m столбцов. Отсортируйте каждую строку матрицы по убыванию методом подсчёта. 
        }
    }
}