using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using OSLab_1.Extensions;

namespace Task2_Randevu
{
    class Program
    {
        static void Main(string[] args)
        {
            SetDefaultConsoleConfigs();

            Console.Write("Введите число элементов ряда n:");
            var str = Console.ReadLine();
            var n = int.Parse(str ?? throw new InvalidOperationException());
            Console.WriteLine();

            Console.Write("Введите число x:");
            var str2 = Console.ReadLine();
            var x = int.Parse(str2 ?? throw new InvalidOperationException());

            var res = Calc_e_x(n, x);
            IndicateToughCalculations(res);
            
            Console.ReadKey();
        }

        private static async void IndicateToughCalculations(Task<double> calcSequence)
        {
            ConsoleEx.WriteColorfulText("Ведутся сложные расчеты ждите", ConsoleColor.Yellow);
            while (!calcSequence.IsCompleted)
            {
                var task = new Action(() => { Thread.Sleep(1000); });

                await Task.Run(task);
                var waitSigns = new[] {'*','*','*','*','*','*','*','*','*','*','*','*','*','*'};
                foreach (var sign in waitSigns)
                {
                    Console.Write(sign);
                    await Task.Delay(30).ConfigureAwait(true);
                }

                GetCaretBack(waitSigns.Length);
                for (var i = 0; i < waitSigns.Length; i++)
                {
                    Console.Write(" ");
                    await Task.Delay(30).ConfigureAwait(true);
                }
                GetCaretBack(waitSigns.Length);
            }
            GetCaretBack(100);
            Console.WriteLine($"Результат: {calcSequence.Result}");
        }

        private static void GetCaretBack(int numberSigns)
        {
            for (int i = 0; i < numberSigns; i++)
            {
                Console.Write("\r");
            }
        }


        private static async Task<double> Calc_e_x(int n, int x)
        {
            var sum = 0d;
            for (var i = 0; i < n; i++)
            {
                var taskFactorial = Task.Run(() => CalcFactorial(i));
                var taskPowX = Task.Run(() => MathPow(x, i));

                // //ожидание двух задач вычисление факториала и возведение в степень
                await Task.WhenAll(taskFactorial, taskPowX);

                sum += taskPowX.Result / taskFactorial.Result;
            }

            Thread.Sleep(5000);
            return sum;
        }

        private static int CalcFactorial(int a)
        {
            var result = 1;
            for (var i = 1; i <= a; i++)
            {
                result *= i;
            }

            return result;
        }


        private static double MathPow(double number, int degree)
        {
            return Math.Pow(number, degree);
        }

        private static void SetDefaultConsoleConfigs()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            ConsoleHelper.SetCurrentFont("Consolas", 28);
        }
    }
}