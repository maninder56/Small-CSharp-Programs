using System.Globalization;

namespace Game; 

public static class UI 
{
    // Explain the basic rules of hte game
    static string rules = """
    You can Choose three Moves That are Attack (A), Defence (D), and Save (S). 

    Attack  : Reduces Enemy's Health.

    Defence : Can protect you from Enemy's Attack.
    
    Save    : To Save your Turn Point to use it in next turn. 
              Using a save in next turn will add multiplier which means, 
              Your Attack will deal more damage and defence will be much stronger!
              However, using Save will leave you defenceless. 

             You can only stack up to 3 save points. 
    """; 

    static string UserInput()
    {
        string userInput = ReadLine() ?? "";
        return userInput; 
    }

    // Displays Main menu 
    public static string MainMenuOption()
    {
        string appName = "The Hunt"; 

        Clear();
        WriteLine();

        CustomMessages.WriteTitle(appName.PadLeft(11)); 
        WriteLine();

        CustomMessages.WriteStandardMessage("Main Menu".PadLeft(12)); 
        WriteLine(); 

        WriteLine("Welcome to The Hunt"); 
        WriteLine();

        WriteLine("To Play Enter 'Play'"); 
        WriteLine("To Exit Enter 'Quit'"); 
        Write("> "); 

        return UserInput(); 
    }

    public static string ShowGameMatchAndGetUerInput(MatchStatus matchStatus)
    {
        // Display main game area via a method that takes 
        // player and enemy's stats 

        ShowGameMatch(matchStatus); 

        WriteLine("Your Turn: "); 
        Write(">"); 
        return UserInput(); 
    }

    public static void ShowGameMatch(MatchStatus matchStatus)
    {
        
        Clear(); 
        WriteLine(); 

        WriteLine($"Rules: {rules}"); 
        WriteLine(new string('-', WindowWidth-10).PadLeft(WindowWidth-5)); 
        WriteLine(); 

        // Temporary, Simple show of stats of enemy and player 
        WriteLine("Enemy Stats: "); 
        WriteLine($"Health: {matchStatus.Enemy.Health}"); 
        WriteLine($"Save Point: {matchStatus.Enemy.Save + 1}"); 
         
        WriteLine(); 

        WriteLine("Player's Stats: "); 
        WriteLine($"Health: {matchStatus.Player.Health}"); 
        WriteLine($"Attack: {matchStatus.Player.Attack}"); 
        WriteLine($"Defence: {matchStatus.Player.Defence}"); 
        WriteLine($"Save Point: {matchStatus.Player.Save + 1}"); 
        WriteLine();
    }
}

// class to build custom messages
public static class CustomMessages
{
    static ConsoleColor MessageKind(MessageType type)
    {
        switch(type)
        {
            case MessageType.Standard: 
            return ConsoleColor.DarkBlue; 

            case MessageType.Good: 
            return ConsoleColor.Green; 

            case MessageType.Bad: 
            return ConsoleColor.Red;

            case MessageType.BadInput: 
            return ConsoleColor.DarkYellow; 

            case MessageType.Title: 
            return ConsoleColor.Magenta; 

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

    static void WriteMessageOneline(MessageType type, string message)
    {
        ForegroundColor = MessageKind(type); 
        Write(message); 
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

    public static void WriteBadMessageOneLine(string message)
    {
        WriteMessageOneline(MessageType.Bad, message); 
    }

    public static void WriteGoodMessage(string message)
    {
        WriteMessage(MessageType.Good, message); 
    }

    public static void WriteBadInputMessage(string message)
    {
        WriteMessage(MessageType.BadInput, message); 
    }

    public static void WriteTitle(string title)
    {
        WriteMessage(MessageType.Title, title); 
    }

}  

public enum MessageType { Standard, Good, Bad, BadInput, Title }



// Prebuild messages
public class MessageNotifications
{
    public static void WrongMainMenuUserInput()
    {
        CustomMessages.WriteBadInputMessage("You only have two Options"); 
        WriteLine("Press Enter"); 
        ReadLine(); 
    }
}


