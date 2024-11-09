using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace Game;

// Note : add a class to only display the current state of applicatoin when reseting the grid 

public static class UI
{
    static string UserInput()
    {
        string userInput = ReadLine() ?? "";
        return userInput; 
    }

    // Displays Main menu 
    public static string MainMenuOption()
    {
        string appName = "Tic-Tac-Toe"; 

        Clear();
        WriteLine();
        WriteLine(appName.PadLeft(20)); 
        WriteLine();
        Messages.WriteStandardMessage("Main Menu".PadLeft(19)); 
        WriteLine(); 
        WriteLine("Welcome to Tic-Tac-Toe"); 
        WriteLine();
        WriteLine("To Play Enter 'Play'"); 
        WriteLine("To Exit Enter 'Quit'"); 
        Write("> "); 

        return UserInput(); 
    }
    
    // Displays Play Area and grid 
    public static string MainGameLoop(Grid grid, string playerName)
    {
        string gameHelp = """ 
            Please give a number BETWEEN 0 to 8 to place your turn. 
            A reference Grid has been provided to let you known where 
            your turn will be places on the grid given a number.  
            """; 

        Clear(); 
        WriteLine(); 
        WriteLine(gameHelp); 
        WriteLine(); 
        WriteUserGrid(grid); 
        WriteLine();
        WriteLine("Player: {0} ", playerName);
        Write("> "); 

        return UserInput(); 

    }

    static void WriteUserGrid(Grid grid)
    {
        bool?[] boxes = grid.Boxes; 

        WriteLine("  User Grid" + new string(' ', 10) + "Reference Grid"); 

        for (int i=0; i <= 8; i +=3)
        {
            string position1 = (boxes[i] == true) ? "X" : (boxes[i] == false) ? "O" : " "; 
            string position2 = (boxes[i+1] == true) ? "X" : (boxes[i+1] == false) ? "O" : " "; 
            string position3 = (boxes[i+2] == true) ? "X" : (boxes[i+2] == false) ? "O" : " "; 

            Write("{0,-2} | {1,-2} | {2,-2}", position1, position2, position3); 

            Write(new string(' ', 6)); 
            
            WriteLine("{0,-2} | {1,-2} | {2,-2}", i, i+1, i+2); 
        }
    }
}


public static class Messages
{
    static ConsoleColor MessageKind(MessageType type)
    {
        switch(type)
        {
            case MessageType.Standard: 
            return ConsoleColor.Blue; 

            case MessageType.Good: 
            return ConsoleColor.Green; 

            case MessageType.Bad: 
            return ConsoleColor.Red;

            case MessageType.BadInput: 
            return ConsoleColor.DarkYellow; 

            default: 
            return ConsoleColor.Gray;   
        }
    }
    static void WriteMessage(MessageType type, string message)
    {
        ForegroundColor = MessageKind(type); 
        WriteLine(message); 
        ResetColor(); 
    }

    public static void WriteSmallMessage(ConsoleColor color, string message)
    {
        ForegroundColor = color; 
        Write(message); 
        ResetColor(); 
    }

    public static void WriteStandardMessage(string message)
    {
        WriteMessage(MessageType.Standard, message); 
    }

    public static void WriteBadMessage(string message)
    {
        WriteMessage(MessageType.Bad, message); 
    }

    public static void WriteGoodMessage(string message)
    {
        WriteMessage(MessageType.Good, message); 
    }

    public static void WriteBadInputMessage(string message)
    {
        WriteMessage(MessageType.BadInput, message); 
    }

}  

public enum MessageType { Standard, Good, Bad, BadInput }