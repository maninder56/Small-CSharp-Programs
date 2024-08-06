using System; 
using static System.Console; 
using System.Threading; 
using GameLibrary;

internal class Program
{
    private static void Main(string[] args)
    {
        UI.WriteGameHeadLine(); 

        int player1 = 0; 
        int player2 = 0; 
        int draw = 0; 

        Task[] taskArray1 = new Task[100_000]; 

        for (int i = 0; i < taskArray1.Length; i++)
        {
            taskArray1[i] = Task.Run(() => {
                Game game = new Game(); 
                if (!game.IsMatchDraw)
                {
                    if (game.Winner == game.player1)
                    {
                        player1++; 
                    }
                    else 
                    {
                        player2++; 
                    }
                }
                if (game.IsMatchDraw)
                {
                    draw++; 
                }
                UI.WriteGameResults(game); 
                }); 
        }

        Task.WaitAll(taskArray1); 
        UI.FinalScore(player1, player2, draw); 
    }
}