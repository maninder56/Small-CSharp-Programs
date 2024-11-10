using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace Game;

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
    public static string MainGameLoop(Grid grid, string playerName, Score score)
    {
        string gameHelp = """ 
            Please give a number BETWEEN 0 to 8 to place your turn. 
            A reference Grid has been provided to let you known where 
            your turn will be places on the grid given a number.  

            To Exit this Match Type 'Quit'. 
            Exiting the Match will Reset the Score. 
            """; 

        Clear(); 
        WriteLine(); 
        WriteLine(gameHelp); 
        WriteLine(); 
        WriteUserGridAndScore(grid, score); 
        WriteLine();
        WriteLine("Player: {0} ", playerName);
        Write("> "); 

        return UserInput(); 

    }

    public static void DisplayGameState(Grid grid, string playerName, Score score)
    {
        string gameHelp = """ 
            Please give a number BETWEEN 0 to 8 to place your turn. 
            A reference Grid has been provided to let you known where 
            your turn will be places on the grid given a number. 

            To Exit this Match Type 'Quit'. 
            Exiting the Match will Reset the Score. 
            """; 

            Clear(); 
            WriteLine(); 
            WriteLine(gameHelp); 
            WriteLine(); 
            WriteUserGridAndScore(grid, score); 
            WriteLine();
            WriteLine("Player: {0} ", playerName);
            Write("> "); 
    }

    static void WriteUserGridAndScore(Grid grid, Score score)
    {
        bool?[] boxes = grid.Boxes; 

        WriteLine($"Score: "); 
        WriteLine("  X  |  O  |  Draw  "); 
        WriteLine("  {0,-2} |  {1,-2} |  {2,2}", score.player1Score, score.player2Score, score.draw); 
        WriteLine(); 

        WriteLine("  User Grid" + new string(' ', 6) + "Reference Grid"); 

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