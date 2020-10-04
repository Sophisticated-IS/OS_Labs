using System;
using System.IO;
using System.Threading;
using CommonMutexLib;
using OSLab_1.Extensions;

namespace N2_File_Read_Write_2
{
    class Program
    {
        static void Main(string[] args)
        { 
          var lastReadLine = 0;
          while (true)
          {
              Thread.Sleep(100);
              
              CommonFileMutex.Mutex.WaitOne();
              ConsoleEx.WriteColorfulText("Reader - захватил мьютекс",ConsoleColor.Green);
              using(var streamReader = new StreamReader(CommonFileMutex.FilePath))
              {
                  var lineNumber = 0;
                  var readString = "";
                  while (lineNumber<= lastReadLine && streamReader.ReadLine()!=null)
                  {
                      lineNumber++;//пропуск уже считанных строк
                  }

                  while ((readString = streamReader.ReadLine())!= null)
                  {
                      ConsoleEx.WriteColorfulText(readString,ConsoleColor.Yellow);
                  }

                  lastReadLine = lineNumber;
              }
              CommonFileMutex.Mutex.ReleaseMutex();
              ConsoleEx.WriteColorfulText("Reader - отпустил мьютекс",ConsoleColor.Green);
          }
          
  
        }
    }
}