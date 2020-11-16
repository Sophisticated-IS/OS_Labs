using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CommonMutexLib;
using OSLab_1.Extensions;

namespace N2_File_Read_Write
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleEx.WriteColorfulText("Press Enter to start input text or press Esc to exit", ConsoleColor.Magenta);
           var mutex = new Mutex(false,"ReadWriteMutex");
            while (true)
            {
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        ConsoleEx.WriteColorfulText("Приложение будет закрыто", ConsoleColor.Red);
                        Environment.Exit(0);
                        break;

                    case ConsoleKey.Enter:
                        WriteTextToFile(mutex);
                        break;
                }
            }
        }


        private static void WriteTextToFile(Mutex mutex)
        {
            ConsoleEx.WriteColorfulText("Введите текст для записи:", ConsoleColor.Yellow);
            var userText = Console.ReadLine();

            mutex.WaitOne();
            ConsoleEx.WriteColorfulText("Writer - захватил мьютекс", ConsoleColor.DarkCyan);
            Thread.Sleep(3000);//типо долго пишем
            using (var streamWriter = new StreamWriter(CommonFileMutex.FilePath, true))
            {
                streamWriter.WriteLine(userText);
            }

            ConsoleEx.WriteColorfulText("Writer - отпустил мьютекс", ConsoleColor.DarkCyan);
            mutex.ReleaseMutex();
        }
    }
}