using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using OSLab_1.Extensions;

namespace HashTable
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите кол-во потоков");
            var n = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            
            Console.WriteLine("Введите основание hash функции");
            var k = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            
            
            var hashT = new CustomHashTable(k);
            var locker = new object();
            Parallel.For((long) 0, n, i =>
                {
                    while (true)
                    {
                        Thread.Sleep(50);
                        var rand = new Random();
                        var key = rand.Next(int.MinValue, int.MaxValue);
                        var value = rand.Next(int.MinValue, int.MaxValue);
                        lock (locker)//TODO
                        {
                            hashT.Insert(key,value);
                        }

                        var msg = $"key: {key}, value: {value}";
                        Console.WriteLine(msg);
                        Thread.Sleep(100);
                    }
                }
            );
        }
    }
}