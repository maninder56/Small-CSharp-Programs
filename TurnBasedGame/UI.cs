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
        // Fix the padding and add section to display rule of the game
        string appName = "The Hunt"; 

        Clear();
        WriteLine();
        WriteLine(appName.PadLeft(20)); 
        WriteLine();
        Messages.WriteStandardMessage("Main Menu".PadLeft(19)); 
        WriteLine(); 
        WriteLine("Welcome to The Hunt"); 
        WriteLine();
        WriteLine("To Play Enter 'Play'"); 
        WriteLine("To Exit Enter 'Quit'"); 
        Write("> "); 

        return UserInput(); 
    }

    public static string MainGameLoop()
    {
        // Display main game area via a method that takes 
        // player and enemy's stats 

        return UserInput(); 

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

    /// <summary>
    /// Message Gets written with next line
    /// </summary>
    /// <param name="type">Type of message</param>
    /// <param name="message"></param>
    static void WriteMessage(MessageType type, string message)
    {
        ForegroundColor = MessageKind(type); 
        WriteLine(message); 
        ResetColor(); 
    }

    /// <summary>
    /// Message gets written without next line
    /// </summary>
    /// <param name="color">Console ForegroundColor</param>
    /// <param name="message"></param>
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