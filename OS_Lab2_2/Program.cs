using System;
using System.Threading.Tasks;
using OSLab_1.Extensions;

namespace OS_Lab2_2
{
    class Program
    {
        static void Main()
        {
            ConsoleEx.WriteColorfulText("Введите текст для нахождения хэш суммы:", ConsoleColor.Yellow);
            var text = Console.ReadLine();

            if (text == null || text.Length <= 0) return;

            var locker = new object();
            var sum = default(int);
            var n = text.Length;
            const int k = 3;
            Parallel.For(0, k, i =>
            {
                var s = 0;

                while (i + k * s < n)
                {
                    lock (locker)
                    {
                        sum += text[s] % 256;
                    }

                    s++;
                }
            });

            ConsoleEx.WriteColorfulText($"Контрольная сумма : {sum}", ConsoleColor.Red);
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