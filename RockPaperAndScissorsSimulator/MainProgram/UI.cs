using static System.Console; 

using GameLibrary;

public static class UI
{
    public static void WriteGameResults(Game game)
    {
        WriteLine("{0}:{1,-10} {2}:{3,-10} {4}", 
            game.player1.Name, game.player1.hand, 
            game.player2.Name, game.player2.hand,
            game.Winner.Name); 

    }

    public static void WriteGameHeadLine()
    {
        WriteLine("Game Results"); 
        WriteLine("{0,-12} {1,-12} {2,-12}", "Player", "Player", "Winner"); 
    }

    public static void FinalScore(int a, int b, int draw)
    {
        WriteLine(); 
        WriteLine($"Number of Times Each Player won:"); 
        WriteLine($"A:{a}"); 
        WriteLine($"B:{b}"); 
        WriteLine($"Number of Draws:{draw}"); 
        WriteLine(); 

        if (a == b)
        {
            WriteLine("Its a Draw"); 
            return; 
        }

        if ( a < b)
        {
            WriteLine($" B won {b-a} more games"); 
            return; 
        }
        WriteLine($" A won {a-b} more games"); 
        
    }
}