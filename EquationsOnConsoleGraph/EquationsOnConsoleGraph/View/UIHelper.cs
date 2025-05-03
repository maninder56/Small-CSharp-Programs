using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Console; 

namespace EquationsOnConsoleGraph.View; 

internal static class UIHelper
{
    private static string refactorUserInput(string? input)
    {
        if (!string.IsNullOrWhiteSpace(input))
        {
            return input.Trim();
        }
        else
        {
            return "";
        }
    }

    internal static string GetUserInput()
    {
        Write($"> ");
        string? userInput = ReadLine();

        return refactorUserInput(userInput);
    }

    internal static void ClearConsoleView() => Clear();

    internal static void WriteLineWithColor(ConsoleColor color, string line)
    {
        ForegroundColor = color;
        WriteLine(line);
        ResetColor();
    }

    internal static void WriteWithColor(ConsoleColor color, string line)
    {
        ForegroundColor = color;
        Write(line);
        ResetColor();
    }
}
