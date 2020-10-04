using System;
using OSLab_1.Extensions;
using TreeTestConsoleApplication;
using System.Linq;
namespace BinaryTree_5
{
    class Program
    {
        /// <summary>
        /// Лабораторная работа по Защите ОС задания №5
        /// Автор: Сова Игорь КМБ
        /// </summary>
        static void Main(string[] args)
        {
            var binaryTree = new BinaryTree<int>(BinaryTree<int>.CompareFunction_Int);
            binaryTree.Add(5);
            binaryTree.Add(2);
            binaryTree.Add(1);
            binaryTree.Add(3);
            binaryTree.Add(3); 
            binaryTree.Add(4);
            binaryTree.Add(6);
            binaryTree.Add(10);
            binaryTree.Add(7);
            binaryTree.Add(8);
            binaryTree.Add(9);
            
            var sum = 0;
            var counter = 0;
            foreach (var treeElt in binaryTree)
            {
                sum += treeElt;
                counter++;
            }
            
            ConsoleEx.WriteColorfulText($"Сумма элементов дерева: {sum}",ConsoleColor.Magenta);
            var avg = (double) sum / counter; 
            ConsoleEx.WriteColorfulText($"Среднее значение = {avg}",ConsoleColor.Yellow);

            Console.ReadLine();
        }
    }
}