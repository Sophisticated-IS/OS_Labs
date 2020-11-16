using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OSLab_1.Extensions;

namespace Task1_LentSort
{
    class Program
    {
        static void Main()
        { 
             var list = new List<int>{9,7,5,0,8,3,5,2};
            // var list = new List<int> {8, 7, 6, 5, 4, 3, 2, 1};
            // var list = new List<int>{2,1};
            // var list = new List<int>{8999};
            var length = list.Count;
            ConsoleEx.WriteColorfulText("Изначальный список:", ConsoleColor.Yellow);
            PrintList(list);
            Console.WriteLine();

            var step = 1;
            while (step < length)
            {
                var firstPart = new int[list.Count / 2];
                var secondPart = new int[list.Count / 2];
                list.CopyTo(0, firstPart, 0, list.Count / 2);
                list.CopyTo(list.Count / 2, secondPart, 0, list.Count / 2);

                //количество шагов по выбору элементов из списка
                var sortedListOnStep = new List<int>(length);
                var indexInTwoArrays = 0;
                // for (var i = 0; i < firstPart.Length/step ; i++)
                // {
                var step1 = step;
                Parallel.For(0, firstPart.Length / step, i =>
                {
                    var arrToSort = new List<int>(step1 * 2);
                    for (var j = 0; j < step1; j++)
                    {
                        arrToSort.Add(firstPart[indexInTwoArrays]);
                        arrToSort.Add(secondPart[indexInTwoArrays]);
                        indexInTwoArrays++;
                    }

                    var buffArray = arrToSort.ToArray();
                    SortMassive(ref buffArray); //выбрали массив и сортируем элементы
                    sortedListOnStep.AddRange(buffArray);
                });

                // }

                //обновим список на новый после сортировки на конкретном шаге
                list = sortedListOnStep;
                step *= 2;
            }


            ConsoleEx.WriteColorfulText("Выходной список:", ConsoleColor.Yellow);
            PrintList(list);
            Console.ReadKey();
        }

        private static void SortMassive(ref int[] massive)
        {
            massive = massive.OrderBy(x => x).ToArray();
        }

        private static void PrintList(IEnumerable<int> list)
        {
            foreach (var elt in list)
            {
                Console.Write(elt + " ");
            }
        }
    }
}
//  var step = 1;
// while (step<length)
// {
// #region copyMasssiveToSort
//
// var buffList = new List<int>(length);
//     for (var i = 0; i < length/2/step; i++)
// {
//     var arr = new int[step * 2];
//     for (var j = 0; j < step + i*step-1; j++)
//     {
//         var ind = 
//             arr[j] = list[j];
//         arr[j + 1] = list[length / 2 + j];
//                    
//         j ++;
//     }
//     SortMassive(ref arr);
//     buffList.AddRange(arr);
// }
//
// list = buffList;
// #endregion
//                 
// step*=2;
// }