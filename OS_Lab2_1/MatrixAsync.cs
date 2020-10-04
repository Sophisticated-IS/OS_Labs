using System;
using System.Threading.Tasks;
using OSLab_1.Extensions;

namespace OS_Lab2
{
    public class MatrixAsync
    {
        public void FillRowRandomNumbers<T>(ref T[,] matrix)
        {
            var firstDim = matrix.GetLength(0);
            var secondDim = matrix.GetLength(1);
            var matrixParallel = matrix;
            Parallel.For((long) 0, firstDim, i =>
                {
                    var rand = new Random();
                    for (var j = 0; j < secondDim; j++)
                    {
                        var val = rand.NextDouble() + rand.Next(0, 100);
                        matrixParallel[i, j] = (T) Convert.ChangeType(val, typeof(T));
                    }
                }
            );

            matrix = matrixParallel;
        }

        public T CountRowSum<T>(T[,] matrix)
        {
            var firstDim = matrix.GetLength(0);
            var secondDim = matrix.GetLength(1);
            dynamic rowsSum = default(T);
            var locker = new object();
            Parallel.For((long) 0, firstDim, i =>
                {
                    dynamic localSum = default(T);
                    for (var j = 0; j < secondDim; j++)
                    {
                        localSum += matrix[i, j];
                    }

                    lock (locker)
                    {
                        rowsSum += localSum;
                    }
                }
            );

#if DEBUG
            dynamic testSum = default(T);
            for (int i = 0; i < firstDim; i++)
            {
                for (int j = 0; j < secondDim; j++)
                {
                    testSum += matrix[i, j];
                }
            }
            ConsoleEx.WriteColorfulText($"Тестовая сумма: {testSum}", ConsoleColor.Red);
#endif
            return rowsSum;
        }
    }
}