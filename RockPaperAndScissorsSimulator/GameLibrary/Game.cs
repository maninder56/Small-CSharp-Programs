// This class represents each game between two players 

namespace GameLibrary; 

public class Game
{
    public bool IsMatchDraw;

    private Player? GetPlayerWon() 
    {
        Player player1 = new Player("A");
        Player player2 = new Player("B");

        if (player1.hand == player2.hand)
        {
           IsMatchDraw = true;  
        }

        else if ((player1.hand == Hand.Rock) && (player2.hand == Hand.Scissor))
        {
            return player1; 
        }

        else if ((player1.hand == Hand.Paper) && (player2.hand == Hand.Rock))
        {
            return player1; 
        }

        else if ((player1.hand == Hand.Scissor) && (player2.hand == Hand.Paper))
        {
            return player1; 
        }

        else 
        {
            if (!IsMatchDraw)
            {
                return player2;
            }
        }

        return new Player("None"); 
    }





}