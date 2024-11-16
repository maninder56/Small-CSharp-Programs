using System.Diagnostics;
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

        bool isInputValid = IsInputValid(userInput, out char? playerMove); 

        if (!isInputValid)
        {
            OnBadInput(new BadInputMessage("Please Only use 'A' for Attack and 'D' for Defence")); 
            return; 
        }

        if (playerMove == 'A')
        {
            enemy.GotAttacked(player.Attack); 

            if (enemy.Health <= 0)
            {
                OnPlayerWon(EventArgs.Empty); 
            }
            return; 
        }

        if (enemy.Health <= 0)
        {
            OnPlayerWon(EventArgs.Empty); 
            return; 
        }

        if (playerMove == 'D')
        {
            WriteLine("not implemented"); ReadLine(); 
            return;
        }

        

    }

    bool IsInputValid(string userInput, out char? currentMove)
    {
        char[] moves = userInput.ToCharArray(); 
        currentMove = null; 

        if (moves.Length > 1)
        {
            return false; 
        }

        switch(moves[0])
        {
            case 'A':
            case 'D': 
            currentMove = moves[0]; 
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