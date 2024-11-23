using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Game; 

public class Match 
{
    // player properties 
    private PlayerClass privatePlayer = new PlayerClass(); 
    private EnemyClass privateEnemy = new EnemyClass(); 

    public PlayerClass Player => privatePlayer; 
    public EnemyClass Enemy => privateEnemy; 



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

        if (!CanPlayerMakeMoreMoves(privatePlayer, currentMove.Length))
        {
            OnBadInput(new BadInputMessage($"You have only have {privatePlayer.Save + 1} Save points to paly.")); 
            return false;    
        }
        
        return true; 
    }


    bool CanPlayerMakeMoreMoves(PlayerClass player, int totalMoves)
    {
        if (player.Save + 1 >= totalMoves)
        {
            return true; 
        }

        return false; 
    }

    void PlayCurrentTurn(char[] playerMove, char[] enemyMove)
    {
        // Total Attack, Defence and savePoint of Player 

    }

    (int totalAttack, int totalDefence, int totalSave) TotalADS(char[] moves)
    {
        int numberofAttack = moves.Select(c => c == 'A').Count();
        int numberOfDefence = moves.Select(c => c == 'D').Count();
        int numberOfSave = moves.Select(c => c == 'S').Count();

        int totalAttack; 
        int totalDefence; 
        int totalSave; 

        if (numberofAttack > 0)
        {
            totalAttack = privatePlayer.Attack; 

            // multipliers will be addes to the rest of the attacts
            if (numberofAttack > 1)
            {
                
            }
        }

        int AddMultiplier(int numberOfAttacksOrDefences, int attackOrDefencePower)
        {
            double multipliers = 0.4; 
            int totalAttack;

            for(int i=0; i < numberOfAttacksOrDefences; i++)
            {

            }
        }



        return (a, d, s); 
    }


    public void MatchRestart()
    {
        privatePlayer = new PlayerClass(); 
        privateEnemy = new EnemyClass(); 
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
    public readonly PlayerClass player; 
    public readonly EnemyClass enemy; 

    public MatchStatus(Match match)
    {
        player = match.Player;  
        enemy = match.Enemy; 
    }
}


public class PlayerClass
{
    int health = 100; 
    int attack = 40; 
    int defence = 30; 
    int save = 0; 

    // for later implementation, change the save to ->
    // int attackPoint = 0; 

    public int Attack => attack; 

    public int Health => health; 

    public int Defence => defence; 

    public int Save => save; 

    public bool TryToAddSingleSave()
    {
        if (save + 1 > 2)
        {
            return false; 
        }

        save += 1;  
        return true; 
    }

    public void GotAttacked(int totalAttack, int totalDefence)
    {
        int reducedAttack = totalAttack - totalDefence; 

        if (totalDefence >= totalAttack)
        {
            reducedAttack = 0;  
        }

        health -= Math.Abs(reducedAttack); 
        
        if (health < 0)
        {
            health = 0; 
        }
    }
}

public class EnemyClass : PlayerClass 
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

