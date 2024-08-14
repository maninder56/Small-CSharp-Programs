using System; 
using static System.Console; 

namespace Tic_Tac_Toe.UI;

public static class MainMenu
{
    public static void UserOption()
    {
        Console.Clear();
        string appName = "Tic-Tac-Toe"; 
        WriteLine(appName.PadLeft(20)); 
        Messages.WriteMessage(ConsoleColor.Blue, "Main Menu".PadLeft(19)); 

        WriteLine("\nTo ")
    }
}

public class Messages
{
    public static void WriteMessage(ConsoleColor color, string message)
    {
        Console.ForegroundColor = color; 
        WriteLine(message); 
        Console.ResetColor(); 
    }
}   