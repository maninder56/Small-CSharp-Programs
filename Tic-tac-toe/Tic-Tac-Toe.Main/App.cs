namespace Game; 
// Note : Add a Scoring System to tell the user final score

public class App 
{
    public void Start()
    {
        bool quit = false; 
        while(!quit)
        {
            // string userInput = UI.MainMenuOption(); 

            // Temporary variable
            string userInput = "Play";

            switch (userInput)
            {
                case "Quit": 
                quit = true; 
                break; 

                case "Play": 
                PlayArea(); 
                break; 
            }
        }
    }

    public void PlayArea()
    {
        bool quit = false; 

        bool player1 = true; // is X 
        bool player2 = false; // is O

        bool firstTurnOfTheGame = player1; // for first turn of The Game 
        bool currentTurn = firstTurnOfTheGame; // for changing turns in Game 

        void changeThePlayer() => currentTurn = (currentTurn == player1) ? player2 : player1;
        void changeThefirstTurnOfTheGame() => firstTurnOfTheGame = (firstTurnOfTheGame == player1) ? player2 : player1; 

        string currentPlaterName() => (currentTurn == true) ? "X" : "O"; 

        // Score 
        int player1Score = 0; 
        int player2Score = 0; 
        int draw = 0;

        Grid grid = new Grid(); 
        grid.ChangeTurn += (object? sender, EventArgs e) => 
        {
           changeThePlayer(); 
        }; 

        grid.GridFilled += (object? sender, EventArgs e) => 
        {
            draw += 1; 
            
            Messages.WriteStandardMessage("The Match has resulted in Draw"); 
            Messages.WriteStandardMessage("Grid will be new reset"); 
            
            changeThefirstTurnOfTheGame(); 
            currentTurn = firstTurnOfTheGame; 
            Messages.WriteStandardMessage($"In the next Game Player: {currentPlaterName()} will start the game."); 

            Messages.WriteGoodMessage("Press Enter To Continue"); 
            ReadLine(); 
            grid.ResetGrid(); 
        }; 

         

        // Main Game loop 
        while(!quit)
        {
            string userInput = UI.MainGameLoop(grid, currentPlaterName()); 

            switch(userInput)
            {
                case "Quit": 
                quit = true; 
                break; 

                default: 
                grid.UpdatePosition(userInput, currentTurn); 
                break; 
            }
            

        } 


    }
}   