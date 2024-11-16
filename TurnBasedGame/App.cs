namespace Game; 

public class App
{
    public void Start()
    {
        bool quit = false; 

        while(!quit)
        {
            // Temporary variable for quick play
            string userInput = "Play"; 

            // string userInput = UI.MainMenuOption(); 

            switch (userInput)
            {
                case "Quit": 
                quit = true; 
                break; 

                case "Play": 
                PlayArea(); 
                break; 

                default: 
                MessageNotifications.WrongMainMenuUserInput(); 
                break; 
            }
        }
    }

    public void PlayArea()
    {
        bool quit = false; 

        Match match = new Match(); 

        // Assign event subscribers for notifications 
        match.BadInput += AppNotifications.InvalidInput; 
        match.PlayerWon += (object? sender, EventArgs e) => UI.ShowGameMatch(new MatchStatus(match)); 
        match.PlayerWon += AppNotifications.PlayerWon; 

        // When player winds the match
        match.PlayerWon += (object? sender, EventArgs e ) => match.MatchRestart(); 
        

        // Main Game loop 
        while(!quit)
        {
            string userInput = UI.ShowGameMatchAndGetUerInput(new MatchStatus(match)); 

            switch(userInput)
            {
                case "Quit": 
                quit = true; 
                break; 

                default: 
                match.UpdateMatch(userInput); 
                break; 
            }
        }
    }
}

public class AppNotifications
{
    // Different messages that user will get after Match rasis some event 
    // Subscriber methods 

    public static void InvalidInput(object? sender, BadInputMessage e)
    {
        CustomMessages.WriteBadInputMessage(e.message); 
        WriteLine("Press Enter");
        ReadLine(); 
    }

    public static void PlayerWon(object? sender, EventArgs e)
    {
        CustomMessages.WriteGoodMessage("Congratulations You Won!!!"); 
        WriteLine("Press Enter to restart"); 
        ReadLine(); 
    }
}