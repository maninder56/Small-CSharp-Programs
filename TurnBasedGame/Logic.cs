using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Principal;

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
    public event EventHandler? PlayerLost; 

    protected virtual void OnBadInput(BadInputMessage e) => BadInput?.Invoke(this, e); 
    protected virtual void OnPlayerWon(EventArgs e) => PlayerWon?.Invoke(this, e); 
    protected virtual void OnPlayerLost(EventArgs e) => PlayerLost?.Invoke(this, e); 



    public void UpdateMatch(string userInput)
    {

        bool isInputValid = IsInputValid(userInput, out char[] playerMove); 

        if (!isInputValid)
        {
            return; 
        }

        PlayCurrentTurn(playerMove, privateEnemy.GetEnemyMove().ToString().ToCharArray()); 

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

        if (moves.Length < 1)
        {
            OnBadInput(new BadInputMessage("No Input Given, Please Choose your Move."));
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
        (int totalAttack, int totalDefence, int totalSave) calculatedPlayerMoves = TotalPlayerADS(playerMove); 
        (int totalAttack, int totalDefence, int totalSave) calculatedEnemyMoves = TotalEnemyADS(enemyMove); 

        // Adjust the save point 
        if (!privatePlayer.TryToAddAllSavePoints(calculatedPlayerMoves.totalSave))
        {
            OnBadInput(new BadInputMessage("You can not add more save points")); 
            return; 
        }
        

        // Total Damage taken By Player
        privatePlayer.GotAttacked(calculatedEnemyMoves.totalAttack, calculatedPlayerMoves.totalDefence); 
        
        // Check Player's health 
        if (privatePlayer.Health <= 0 )
        {
            OnPlayerLost(EventArgs.Empty); 
            return; 
        }


        // Total Damage taken by Enemy 
        privateEnemy.GotAttacked(calculatedPlayerMoves.totalAttack, calculatedEnemyMoves.totalDefence); 

        if (privateEnemy.Health <= 0)
        {
            OnPlayerWon(EventArgs.Empty); 
            return; 
        }


    }

    (int totalAttack, int totalDefence, int totalSave) TotalPlayerADS(char[] moves)
    {
        int numberofAttack = moves.Where(c => c == 'A').Count();
        int numberOfDefence = moves.Where(c => c == 'D').Count();
        int numberOfSave = moves.Where(c => c == 'S').Count();

        int totalAttack = 0; 
        int totalDefence = 0; 
        int totalSave = numberOfSave; 

        // Check how many times player uses Attack and add multiplier
        if (numberofAttack > 0)
        {
            totalAttack = privatePlayer.Attack; 

            // a multipliers will be added to the rest of the attacts
            if (numberofAttack > 1)
            {
                totalAttack += AddMultiplier(numberofAttack, privatePlayer.Attack); 
            }
        }


        // Check for the number of times plyaer uses defence
        if (numberOfDefence > 0)
        {
            totalDefence = privatePlayer.Defence; 

            
            if (numberOfDefence > 1)
            {
                totalDefence += AddMultiplier(numberOfDefence, privatePlayer.Defence); 
            }
        }

        int AddMultiplier(int numberOfAttacksOrDefences, int baseNumber)
        {
            double multipliers = 0.4; 
            double totalAttackorDefenceAfterBaseAttack = baseNumber; 

            for(int i=0; i < numberOfAttacksOrDefences; i++)
            {
                privatePlayer.TakeSavePoint(); 
                totalAttackorDefenceAfterBaseAttack += multipliers * totalAttackorDefenceAfterBaseAttack; 
            }

            return (int)totalAttackorDefenceAfterBaseAttack; 
        }

        Debug.WriteLine("Attack: {0}, Defence: {1}, save : {2}", totalAttack, totalDefence, totalSave); 

        return (totalAttack, totalDefence, totalSave); 
    }


    (int totalAttack, int totalDefence, int totalSave) TotalEnemyADS(char[] moves)
    {
        int numberofAttack = moves.Where(c => c == 'A').Count();
        int numberOfDefence = moves.Where(c => c == 'D').Count();
        int numberOfSave = moves.Where(c => c == 'S').Count();

        int totalAttack = 0; 
        int totalDefence = 0; 
        int totalSave = numberOfSave; 

        // Check how many times player uses Attack and add multiplier
        if (numberofAttack > 0)
        {
            totalAttack = privateEnemy.Attack; 

            // a multipliers will be added to the rest of the attacts
            if (numberofAttack > 1)
            {
                totalAttack += AddMultiplier(numberofAttack, privateEnemy.Attack); 
            }
        }


        // Check for the number of times plyaer uses defence
        if (numberOfDefence > 0)
        {
            totalDefence = privateEnemy.Defence; 

            
            if (numberOfDefence > 1)
            {
                totalDefence += AddMultiplier(numberOfDefence, privateEnemy.Defence); 
            }
        }

        int AddMultiplier(int numberOfAttacksOrDefences, int baseNumber)
        {
            double multipliers = 0.4; 
            double totalAttackorDefenceAfterBaseAttack = baseNumber; 

            for(int i=0; i < numberOfAttacksOrDefences; i++)
            {
                privateEnemy.TakeSavePoint(); 
                totalAttackorDefenceAfterBaseAttack += multipliers * totalAttackorDefenceAfterBaseAttack; 
            }

            return (int)totalAttackorDefenceAfterBaseAttack; 
        }

        return (totalAttack, totalDefence, totalSave); 
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

    public bool TryToAddAllSavePoints(int numberOfSavePoint)
    {
        for(int i=0; i < numberOfSavePoint; i++)
        {
            if (!TryToAddSingleSave())
            {
                return false; 
            }
        }

        return true; 
    }

    public void TakeSavePoint()
    {
        if (save - 1 >= 0)
        {
            save -= 1; 
        }
    }

    public void GotAttacked(int totalAttackRecieved, int totalDefenceMaintained)
    {
        Debug.WriteLine("totalAttacRecieved: {0}, totalDefence Minatained:{1}", totalAttackRecieved, totalDefenceMaintained); 
        int reducedAttack = totalAttackRecieved - totalDefenceMaintained; 

        Debug.WriteLine("reduced Attack : {0}", reducedAttack); 

        if (totalDefenceMaintained >= totalAttackRecieved)
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
    // Make a event to notify what move enemy has choosen. 
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

