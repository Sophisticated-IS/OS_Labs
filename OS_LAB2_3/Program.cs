using System;
using System.Collections;
using System.Threading.Tasks;
using OSLab_1.Extensions;

namespace OS_LAB2_3
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO какая f(x)
            ConsoleEx.WriteColorfulText(
                "Дана функция y = f(x), принимающая на отрезке [a,b] только положительные значения"
                , ConsoleColor.DarkMagenta);


            int a;
            int b;
            while (true)
            {
                ConsoleEx.WriteColorfulText("Введите значение a", ConsoleColor.Yellow);
                a = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                ConsoleEx.WriteColorfulText("Введите значение b", ConsoleColor.Yellow);
                b = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                if (a >= 0 && a <= b)
                {
                    break;
                }
                else
                {
                    ConsoleEx.WriteColorfulText("Отрезок введен неверно!", ConsoleColor.Red);
                }
            }

            double h, s = 0, n, k, f;
            n = 1000;
            h = (b - a) / n;

            // double x1 = default, x = default;
            // Parallel.For(x1, x <= b, x =>
            // {
                // if (!(x < b))
                // {
                //     x1 = x + h / 2;
                //     if (x1 >= 2)
                //     {
                //         f = Math.Pow(x1, 2) * Math.Sqrt(4 - x1 * x1);
                //         s += f;
                //     }
                // }
            // });
            int ff = default, d = default;
            Parallel.For(ff, d, i => {});
            for (double x1, x = a; x <= b; x += h)
            {
                if (!(x < b)) continue;
                x1 = x + h / 2;
                if (x1 >= 2)
                    continue;
                lock (new object())
                {
                    f = GetFuncValue(x1);
                }
                
                s += f;
            }
            k = Math.Abs(Math.PI - s * h);
            static double Functor(double x) => x*x+2;
            var result = LeftTriangle(Functor, a, b, 1000);
            ConsoleEx.WriteColorfulText("**********************************", ConsoleColor.Blue);
            ConsoleEx.WriteColorfulText($"Данная площадь равна: {result:0.0000}", ConsoleColor.Magenta);

            // Console.WriteLine();
            // ConsoleEx.WriteColorfulText($"Точность вычисления: {k:0.0000}", ConsoleColor.Green);

            Console.Read();
        }

        private static double GetFuncValue(double x)
        {
            return checked(Math.Pow(x, 2) + 1);
        }
        
        static double LeftTriangle(Func<double, double> f, double a, double b, int n)
        {
            var h = (b - a) / n;
            var sum = 0d;
            for(var i = 0; i <= n-1; i++)
            {
                var x = a + i * h;
                sum += f(x);
            }

            var result = h * sum;
            return result;
        }
    }
}