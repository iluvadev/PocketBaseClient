using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketBaseClient.CodeGenerator
{
    public static class ConsoleExtensions
    {
        public static void WriteDone()
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" >> Done!");
            Console.ForegroundColor = color;
        }
        public static void WriteProcess(string process)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($" -> {process}...");
            Console.ForegroundColor = color;
        }
        public static void WriteError(string error)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($" >> {error}");
            Console.ForegroundColor = color;
        }
    }
}
