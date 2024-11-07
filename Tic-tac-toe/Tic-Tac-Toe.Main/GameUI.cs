namespace Game;

public static class UI
{
    static string UserInput()
    {
        string userInput = ReadLine() ?? "";
        return userInput; 
    }
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

        return UserInput(); 
    }
}

public static class Messages
{
    static ConsoleColor MessageKind(MessageType type)
    {
        switch((int)type)
        {
            case 0: 
            return ConsoleColor.Blue; 

            case 1: 
            return ConsoleColor.Green; 

            case 2: 
            return ConsoleColor.Red;

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
}  

public enum MessageType { Standard, Good, Bad }