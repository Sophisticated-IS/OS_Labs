using System;
using System.ComponentModel.Design;
using System.Text;
using System.Threading;

namespace Task1_ReadersPull
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = 75;
            var buffPull = new BufferPull(n);

            //считыватели из файла каждый в новом потоке
            var readersToPull = new ReaderFileToBuffer[]
            {
                new ReaderFileToBuffer(buffPull){FileNameToRead = "File1.txt"}, 
                new ReaderFileToBuffer(buffPull){FileNameToRead = "File2.txt"}, 
                new ReaderFileToBuffer(buffPull){FileNameToRead = "File3.txt"} 
            };
            while (true)
            {
                // Thread.Sleep(500);
                //когда буфер заполнен хотя бы чем-то его надо считать
                if (buffPull.Buffer.Count == n)
                {
                    Console.WriteLine(buffPull.ReadText());
                }
                else
                {
                    
                }
                    
            }
            
        }
    }
}