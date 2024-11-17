using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Game; 

public class Match 
{
    // player properties 
    public Player player = new Player(); 
    public Enemy enemy = new Enemy(); 

    // Events 
    public event EventHandler<BadInputMessage>? BadInput;
    public event EventHandler? PlayerWon;  

    protected virtual void OnBadInput(BadInputMessage e) => BadInput?.Invoke(this, e); 
    protected virtual void OnPlayerWon(EventArgs e) => PlayerWon?.Invoke(this, e); 



    public void UpdateMatch(string userInput)
    {
        // Note for future implementation: count how many A and D you get to inclrease the attck or defence 
        // first count the defence and then attack 

        bool isInputValid = IsInputValid(userInput, out char[] playerMove); 

        if (!isInputValid)
        {
            return; 
        }

        




        

    }

    bool IsInputValid(string userInput, out char[] currentMove)
    {
        char[] moves = userInput.ToCharArray(); 
        currentMove = new char[moves.Length]; 

        if (moves.Length > 3)
        {
            OnBadInput(new BadInputMessage("You can only have Maximum of 3 moves")); 
            return false; 
        }

        for(int i=0; i < moves.Length; i++)
        {
            switch(moves[i])
            {
                case 'A':
                case 'D': 
                case 'S': 
                currentMove[i] = moves[i]; 
                break; 

                default: // user input contains other characters 
                OnBadInput(new BadInputMessage("Please Only use 'A' for Attack, 'D' for Defence and 'S' for Save")); 
                return false; 
            }
        }

        if (!CanPlayerMakeMoreMoves(player, currentMove.Length))
        {
            OnBadInput(new BadInputMessage($"You have only have {player.Save + 1} Save points to paly.")); 
            return false;    
        }
        
        return true; 
    }


    bool CanPlayerMakeMoreMoves(Player player, int totalMoves)
    {
        if (player.Save + 1 >= totalMoves)
        {
            return true; 
        }

        return false; 
    }



    public void MatchRestart()
    {
        player = new Player(); 
        enemy = new Enemy(); 
    }
}

public class BadInputMessage : EventArgs
{
    public string message; 
    public BadInputMessage(string message) => this.message = message; 
}





// To send Match Information to the UI class 
public class MatchStatus 
{
    public Player player; 
    public Enemy enemy; 

    public MatchStatus(Match match)
    {
        player = match.player; 
        enemy = match.enemy; 
    }
}


public class Player
{
    int health = 100; 
    int attack = 40; 
    int defence = 30; 
    int save = 0; 

    // for later implementation 
    // int attackPoint = 0; 

    public int Attack 
    { 
        get => attack; 
    }

    public int Health 
    {
        get => health; 
    }

    public int Defence
    {
        get => defence; 
    }

    public int Save 
    {
        get => save; 
    }

    public void AddSingleSave()
    {
        if (save + 1 > 2)
        {
            return; 
        }

        save += 1;  
    }



    public void GotAttacked(int attackPower)
    {
        health -= attackPower; 
        
        if (health < 0)
        {
            health = 0; 
        }
    }

    public void GotAttackedWithDefence(int attackPower)
    {
        int reducedAttack = defence - attack; 

        health -= Math.Abs(reducedAttack); 
        
        if (health < 0)
        {
            health = 0; 
        }
    }
}

public class Enemy : Player 
{
    Random random = new Random(); 
    
    public char GetEnemyMove()
    {
        int move = random.Next(0,2); 

        switch (move)
        {
            case 0: 
            return 'A'; 

            default: 
            return 'D'; 
        }

    }
}