using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Task3_NotDecreasingSequance
{
    class Program
    {
        private static double?[] _numbersBuffer = new double?[2];
        private static object _locker = new object();
        private static Thread _readerThread1; 
        private static Thread _readerThread2; 
        static void Main()
        {
           _readerThread1 = new Thread(ReadNumberFromFile);
           _readerThread2 = new Thread(ReadNumberFromFile);
           var writerThread = new Thread(WriteNumbersToFile);
           
           _readerThread1.Start( new Tuple<int,string>(0,"File1.txt"));
           _readerThread2.Start(new Tuple<int,string>(1,"File2.txt"));
           writerThread.Start();

           Console.ReadLine();
        }

        private static void ReadNumberFromFile(object fileName)
        {
            var fileNameThreadID = (Tuple<int,string>) fileName;

            using (var streamReader = new StreamReader(fileNameThreadID.Item2))
            {
                string line;
                while ((line = streamReader.ReadLine())!=null)
                {
                    var numbers = line.Split(' ').Select(x => double.Parse(x)).ToArray();

                    foreach (var number in numbers)
                    {
                        while (true)
                        {
                            lock (_locker)
                            {
                                if (_numbersBuffer[fileNameThreadID.Item1] is null)
                                {
                                    _numbersBuffer[fileNameThreadID.Item1] = number;
                                    break;
                                }
                                else //значение было записано но не было еще удалено
                                {
                                    
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void WriteNumbersToFile()
        {
            while (_readerThread1.IsAlive || _readerThread2.IsAlive)
            {
                lock (_locker)
                {
                    //пока оба потока не запишут 
                    if (_numbersBuffer[0]== null || _numbersBuffer[1] == null) continue;

                    var sb = new StringBuilder();
                    _numbersBuffer.Select(n => sb.Append(n+" ")).ToArray();
                    var str = sb.ToString();
                    using (var streamWriter = new StreamWriter("CommonOutputFile.txt",true))
                    {
                        streamWriter.Write(str);
                    }

                    _numbersBuffer[0] = null;
                    _numbersBuffer[1] = null;
                }
            }

            Console.WriteLine("Запись в общий файл закончена");
            var text = File.ReadAllText("CommonOutputFile.txt").TrimEnd(' ');
            Console.WriteLine("Выходной слитый файл:");
            Console.WriteLine(text);
            Console.WriteLine("Отсортированная общая неубывающая последовательность:");
            var numbers = text.Split(' ').Select(double.Parse).OrderBy(x=>x);
            foreach (var number in numbers)
            {
                Console.Write(number+" ");
            }
        }
        
    }
}