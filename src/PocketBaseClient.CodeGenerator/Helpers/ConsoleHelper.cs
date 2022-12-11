// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

namespace PocketBaseClient.CodeGenerator.Helpers
{
    public static class ConsoleHelper
    {
        public static void WriteDone()
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  >> Done!");
            Console.ForegroundColor = color;
        }
        public static void WriteFailed(string error)
        { 
            WriteError("Failed!");
            WriteError(error);
        }

        public static void WriteProcess(string process)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"  · {process}...");
            Console.ForegroundColor = color;
        }
        public static void WriteError(string error)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  >> {error}");
            Console.ForegroundColor = color;
        }
        public static void WriteEmphasis(string text)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(text);
            Console.ForegroundColor = color;
        }

        public static void WriteCurrentValue(string text, string? currentValue)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"  {text} ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(currentValue);
            Console.ForegroundColor = color;
        }

        public static void WriteStep(int number, string text, string? currentValue = null)
        {
            Console.WriteLine();
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"· Step {number}");
            Console.ForegroundColor = color;

            WriteCurrentValue(text, currentValue);
        }

    }
}
