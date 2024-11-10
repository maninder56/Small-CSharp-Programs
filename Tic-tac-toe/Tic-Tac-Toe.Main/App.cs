namespace Game; 

public class App 
{
    public void Start()
    {
        bool quit = false; 
        while(!quit)
        {
            string userInput = UI.MainMenuOption(); 

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

        string currentPlayerName() => (currentTurn == true) ? "X" : "O"; 

        // Score 
        Score score = new Score(); 
    
        // Game Grid
        Grid grid = new Grid(); 

        grid.ChangeTurn += (object? sender, EventArgs e) => 
        {
           changeThePlayer(); 
        }; 

        grid.GridFilled += (object? sender, EventArgs e) => 
        {
            score.draw += 1; 
            UI.DisplayGameState(grid, currentPlayerName(), score); 

            Messages.WriteBadMessage("The Match has resulted in Draw"); 
            Messages.WriteStandardMessage("Grid will be now reset"); 
            
            changeThefirstTurnOfTheGame(); 
            currentTurn = firstTurnOfTheGame; 
            Messages.WriteStandardMessage($"In the next Game Player: {currentPlayerName()} will start the game."); 

            Messages.WriteGoodMessage("Press Enter To Continue"); 
            ReadLine(); 
            grid.ResetGrid(); 
        }; 

        grid.PlayerWon += (object? sender, PlayerWonEventArgs e) => 
        {
            if (e.player)
            {
                score.player1Score += 1; 
            }

            if(!e.player)
            {
                score.player2Score +=1; 
            }

            UI.DisplayGameState(grid, currentPlayerName(), score); 

            Messages.WriteGoodMessage($"congratulation Player: {currentPlayerName()} You have won!"); 
            Messages.WriteStandardMessage("Grid will be now reset for Next Game."); 
            
            changeThefirstTurnOfTheGame(); 
            currentTurn = firstTurnOfTheGame; 
            Messages.WriteStandardMessage($"In the next Game Player: {currentPlayerName()} will start the game."); 
            Messages.WriteGoodMessage("Press Enter To Continue"); 
            ReadLine(); 
            grid.ResetGrid(); 
        }; 

         

        // Main Game loop 
        while(!quit)
        {
            string userInput = UI.MainGameLoop(grid, currentPlayerName(), score); 

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