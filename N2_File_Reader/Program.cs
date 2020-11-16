using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using CommonMutexLib;
using OSLab_1.Extensions;

namespace N2_File_Read_Write_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var mutex = new Mutex(false,"ReadWriteMutex");

            while (true)
            {
                 Thread.Sleep(100);
                mutex.WaitOne();
                ConsoleEx.WriteColorfulText("Reader - захватил мьютекс", ConsoleColor.Green);

                using (var streamReader = new StreamReader(CommonFileMutex.FilePath))
                {
                    string readString;
                    while ((readString = streamReader.ReadLine()) != null)
                    {
                        ConsoleEx.WriteColorfulText(readString, ConsoleColor.Yellow);
                    }
                }

                mutex.ReleaseMutex();
                ConsoleEx.WriteColorfulText("Reader - отпустил мьютекс", ConsoleColor.Green);
            }
        }
    }
}