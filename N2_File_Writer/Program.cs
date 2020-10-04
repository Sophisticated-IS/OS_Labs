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
        public static event EventHandler TextWasWrittenEvent;
        
        static void Main(string[] args)
        {
            try
            {
                ConsoleEx.WriteColorfulText("Press Enter to start input text or press Esc to exit",ConsoleColor.Magenta);
                TextWasWrittenEvent+= OnTextWasWrittenEvent;
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
                            WriteTextToFile();
                            break;
      
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Thread.Sleep(100000);
                throw;
            }
        }

        private static void OnTextWasWrittenEvent(object? sender, EventArgs e)
        {
            CommonFileMutex.Mutex.ReleaseMutex();
            ConsoleEx.WriteColorfulText("Writer - отпустил мьютекс",ConsoleColor.DarkCyan);
        }

        private static void WriteTextToFile()
        {
            ConsoleEx.WriteColorfulText("Введите текст для записи:", ConsoleColor.Yellow);
            var userText = Console.ReadLine();

            
            CommonFileMutex.Mutex.WaitOne();
            using(var streamWriter = new StreamWriter(CommonFileMutex.FilePath,true))
            {
                ConsoleEx.WriteColorfulText("Writer - захватил мьютекс",ConsoleColor.DarkCyan);
                var writeTask = streamWriter.WriteLineAsync(userText);
                writeTask.ContinueWith(TargetReleaseMutex);
            }
            
        }

        private static void TargetReleaseMutex(Task arg1)
        {
            Thread.Sleep(100);
            TextWasWrittenEvent?.Invoke(null,EventArgs.Empty);
        }
    }
}