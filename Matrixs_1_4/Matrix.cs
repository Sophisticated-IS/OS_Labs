using System;
using System.Collections.Generic;
using System.Linq;
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
        public T[,] GetRandomMatrix<T>(int firstDim, int secondDim)
        {
            var rand = new Random();

            var matrix = new T[firstDim, secondDim];
            Parallel.For((long) 0, firstDim, i =>
                {
                    for (var j = 0; j < secondDim; j++)
                    {
                        var val = rand.NextDouble() + rand.Next(0, 100);
                        matrix[i, j] = (T) Convert.ChangeType(val,typeof(T)) ;
                    }
                }
            );
            return matrix;
        }

        /// <summary>
        /// Сортировка матрицы по строчнометодом подсчета
        /// </summary>
        /// <param name="matrix">масссив</param>
        /// <param name="startInterval">начальный диапазон значений</param>
        /// <param name="endInterval">конечный диапазон значений</param>
        /// <param name="sortOrder">порядок сортировки</param>
        public void SortMatrixByRow(ref int[,] matrix, int startInterval, int endInterval, SortOrder sortOrder)
        {
            var firstDimLength = matrix.GetLength(1);
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                var array = new int[firstDimLength];
                for (var j = 0; j < firstDimLength; j++)
                {
                    array[j] = matrix[i, j];
                }

                CountingSort(ref array, startInterval, endInterval, sortOrder);
                CopyRowToTwoDimensionArray(ref array, ref matrix, i);
            }
        }

        /// <summary>
        /// Generic метод печати массивов двумерных
        /// </summary>
        public void Print<T>(T[,] matrix)
        {
            Console.WriteLine();

            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write("{0:0.##}\t", matrix[i, j]);
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Сортировка вещественных чисел методом подсчета
        /// </summary>
        public void CountingSortDoubleByRow(ref double[,] matrix)
        {
            var countingElements = new Dictionary<double, int>();
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                ResetDictionaryValues(ref countingElements);
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    if (!countingElements.ContainsKey(matrix[i, j]))
                    {
                        countingElements.Add(matrix[i, j], 0);
                    }

                    countingElements[matrix[i, j]]++;
                }

                SetSortedValuesForMatrixRow(ref matrix, i, ref countingElements);
            }
        }

        public void MultipleOnCoefficient(ref int[,] matrix, int coeff)
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i,j] = checked(coeff * matrix[i,j]);
                }
            }
        }
        
        private void SetSortedValuesForMatrixRow(ref double[,] matrix, int rowIndex, ref Dictionary<double, int> values)
        {
            if (matrix == null) throw new ArgumentNullException(nameof(matrix));
            if (values == null) throw new ArgumentNullException(nameof(values));

            //отсортированные ключи по убыванию
            var keyValues = values.Select(x => x.Key)
                .OrderByDescending(key => key).ToArray();
            var dimLength = matrix.GetLength(1);

            var matrixIndex = 0;
            foreach (var keyValue in keyValues)
            {
                for (var i = 0; i < values[keyValue]; i++)
                {
                    matrix[rowIndex, matrixIndex] = keyValue;
                    matrixIndex++;
                }
            }
        }

        private void ResetDictionaryValues(ref Dictionary<double, int> dictionary)
        {
            var keys = dictionary.Keys.ToArray();
            for (var i = 0; i < dictionary.Count; i++)
            {
                dictionary[keys[i]] = 0;
            }
        }

        /// <summary>
        /// Функция сортировки одномерного массива подсчетом
        /// </summary>
        /// <param name="massive">масссив</param>
        /// <param name="startInterval">начальный диапазон значений</param>
        /// <param name="endInterval">конечный диапазон значений</param>
        /// <param name="sortOrder">порядок сортировки</param>
        private void CountingSort(ref int[] massive, int startInterval, int endInterval, SortOrder sortOrder)
        {
            if (massive == null) throw new ArgumentNullException(nameof(massive));
            if (endInterval < startInterval)
                throw new ArgumentOutOfRangeException(nameof(startInterval) + '➜' + nameof(endInterval));

            var dictValuesRange = new Dictionary<int, int>();
            var index = 0;
            for (var i = startInterval; i < endInterval + 1; i++)
            {
                dictValuesRange.Add(i, index);
                index++;
            }


            var counterMassive = new double[endInterval - startInterval + 1];
            for (var i = 0; i < massive.Length; i++)
            {
                counterMassive[dictValuesRange[massive[i]]]++;
            }

            int arrInd;
            switch (sortOrder)
            {
                case SortOrder.Asc:
                    arrInd = 0;
                    for (var i = 0; i < counterMassive.Length; i++)
                    {
                        for (var j = 0; j < counterMassive[i]; j++)
                        {
                            massive[arrInd] = dictValuesRange[i];
                            arrInd++;
                        }
                    }

                    break;
                case SortOrder.Desc:
                    arrInd = 0;
                    for (var i = counterMassive.Length - 1; i >= 0; i--)
                    {
                        for (var j = 0; j < counterMassive[i]; j++)
                        {
                            massive[arrInd] = dictValuesRange[i];
                            arrInd++;
                        }
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(sortOrder), sortOrder, null);
            }
        }

        private void CopyRowToTwoDimensionArray(ref int[] fromMassive, ref int[,] toMassive, int i)
        {
            if (fromMassive == null) throw new ArgumentNullException(nameof(fromMassive));
            if (toMassive == null) throw new ArgumentNullException(nameof(toMassive));

            for (var j = 0; j < toMassive.GetLength(1); j++)
            {
                toMassive[i, j] = fromMassive[j];
            }
        }
    }
}