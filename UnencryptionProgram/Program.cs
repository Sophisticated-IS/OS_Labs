using System;
using System.IO;
using System.Text;
using System.Threading;
using OSLab_1.Extensions;

namespace UnencryptionProgram
{
    class Program
    {
        private static Mutex _mutexStartUnencryption = new Mutex(false,"UnencryptionMutex");

        static void Main(string[] args)
        {
            ConsoleEx.WriteColorfulText("Программа дешифровщика ОЖИДАЕТ зашифрованный файл...",ConsoleColor.Yellow);
            _mutexStartUnencryption.WaitOne();
            ConsoleEx.WriteColorfulText("Программа дешифровщика начала расшифровывать текст",ConsoleColor.Yellow);
            Console.WriteLine("Текст:");
            ConsoleEx.WriteColorfulText(UnencryptText(),ConsoleColor.Cyan);
            _mutexStartUnencryption.Dispose();
            Console.ReadLine();
        }

        private static string UnencryptText()
        {
            var stringBuild = new StringBuilder();
            foreach (var symbol in File.ReadAllText(@"C:\Users\Sova IS\RiderProjects\OSLab_1\Task2_CryptEnglishText\OutputCryptedText.txt"))
            {
                stringBuild.Append(UnencryptSymbol(symbol));
            }

            return stringBuild.ToString();
        }
        
        private static char UnencryptSymbol(char symbol)
        {
            symbol--;
            var resChar = char.IsLower(symbol)
                ? char.ToUpper(symbol)
                : char.ToLower(symbol);
            
            return resChar;
        }
    }
}