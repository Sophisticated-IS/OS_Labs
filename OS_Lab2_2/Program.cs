using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSLab_1.Extensions;

namespace OS_Lab2_2
{
    class Program
    {
        
        
        static void Main()
        {
            ConsoleEx.WriteColorfulText("Введите текст для нахождения хэш суммы:",ConsoleColor.Yellow);
            var text = Console.ReadLine();

            if (text == null || text.Length <= 0) return;
            
            var locker = new object();
            var locker2 = new object();
            var sum = default(int);
            var hash = new StringBuilder();
            var hash2 = new StringBuilder();
            Parallel.For(0,text.Length-1,i =>
            {
                var localSum = default(int);
                for (var j = 0; j < text.Length; j++)
                {
                    var k = text.Length - 1;
                    var s = j;
                    var charNumber = i + k * s;
                    
                    if (j == charNumber % 0xFF)
                    {
                        lock (locker)
                        {
                             sum += text[j];
                             localSum += text[j];
                        }
                    }
                }

                lock (locker2)
                {
                    var res = IntToBytes(localSum);
                    var s = res.Select(b=> (char)b).ToArray();
                    
                    hash.Append(s);
                    hash2.Append((char) localSum);
                }
            });
            
            ConsoleEx.WriteColorfulText($"Контрольная сумма : {sum}",ConsoleColor.Red);
            ConsoleEx.WriteColorfulText($"Контрольная сумма : {hash}",ConsoleColor.Green);
            ConsoleEx.WriteColorfulText($"Контрольная сумма : {hash2}",ConsoleColor.Green);
          Console.Read();

        }

        private static byte[] IntToBytes(int intValue)
        {
            var intBytes = BitConverter.GetBytes(intValue);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(intBytes);
            return intBytes;
        }
    }
}