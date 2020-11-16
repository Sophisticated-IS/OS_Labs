using System;
using System.Drawing;

namespace OSLab_1.Extensions
{
    public static class ConsoleEx
    {
        public static void WriteColorfulText(string message,ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }
    }
}