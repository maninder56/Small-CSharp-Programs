using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Xml.XPath;

namespace Game; 

public class Match 
{
    // player properties 
    private PlayerClass privatePlayer = new PlayerClass(30, 30); 
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

        PlayCurrentTurn(playerMove, privateEnemy.GetEnemyMove()); 

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

        if (!privateEnemy.TryToAddAllSavePoints(calculatedEnemyMoves.totalSave))
        {
            OnBadInput(new BadInputMessage("Enemy has played an illegal move, got more SAVE")); 
            return; 
        }

        // Reveal Enemy's Move 
        CustomMessages.WriteBadMessageOneLine("\nEnemy's Choise: ");
        Array.ForEach(Enemy.EnemyChoise, Write);
        WriteLine("\nPress Enter To See the result."); 
        ReadLine(); 
        


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
        char[] movesWithoutSave = moves
            .Where(c => c == 'A' || c =='D')
            .Select(c => c).ToArray(); 
        
        for(int i=0; i < movesWithoutSave.Length; i++)
        {
            if (i == 0)
            {
                switch(movesWithoutSave[i])
                {
                    case 'A':
                    totalAttack = privatePlayer.Attack; 
                    break; 

                    case 'D':
                    totalDefence = privatePlayer.Defence; 
                    break; 
                }

                continue; 
            }

            switch(movesWithoutSave[i])
            {
                case 'A':
                totalAttack += AddMultiplier(1, privatePlayer.Attack);
                break; 

                case 'D':
                totalDefence += AddMultiplier(1, privatePlayer.Defence); 
                break; 
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
        char[] movesWithoutSave = moves
            .Where(c => c == 'A' || c =='D')
            .Select(c => c).ToArray(); 
        
        for(int i=0; i < movesWithoutSave.Length; i++)
        {
            if (i == 0)
            {
                switch(movesWithoutSave[i])
                {
                    case 'A':
                    totalAttack = privateEnemy.Attack; 
                    break; 

                    case 'D':
                    totalDefence = privateEnemy.Defence; 
                    break; 
                }

                continue; 
            }

            switch(movesWithoutSave[i])
            {
                case 'A':
                totalAttack += AddMultiplier(1, privateEnemy.Attack);
                break; 

                case 'D':
                totalDefence += AddMultiplier(1, privateEnemy.Defence); 
                break; 
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



    public void MatchRestart(int level)
    {
        privatePlayer = new PlayerClass(); 
        
        privateEnemy = new EnemyClass(level * 100); // health is 100  
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
    public readonly PlayerClass Player; 
    public readonly EnemyClass Enemy; 

    public MatchStatus(Match match)
    {
        Player = match.Player;  
        Enemy = match.Enemy; 
    }
}


public class PlayerClass
{
    int health = 100; 
    int attack = 20; 
    int defence = 20; 
    int save = 0; 

    // for later implementation, change the save to ->
    // int attackPoint = 0; 

    public int Attack => attack; 

    public int Health => health; 

    public int Defence => defence; 

    public int Save => save; 

    public PlayerClass() {}
    public PlayerClass(int startingHealth) => health = startingHealth;  
    public PlayerClass(int startingAttack, int startingDefence)
    {
        attack = startingAttack; 
        defence = startingDefence; 
    }

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
        int totalPointsAdded = 0; 
        for(int i=0; i < numberOfSavePoint; i++)
        {
            if (!TryToAddSingleSave())
            {
                save -= totalPointsAdded; 
                return false; 
            }

            totalPointsAdded++; 
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
    public EnemyClass() {}
    public EnemyClass(int startingHealth) : base(startingHealth) {}
    // Make a event to notify what move enemy has choosen. 
    Random random = new Random(); 

    char[] enemyChoise = new char[3];

    public char[] EnemyChoise => enemyChoise; 
    
    public char[] GetEnemyMove()
    {
        char[] result = new char[3]; 

        for(int i=0; i < Save +1; i++)
        {
            result[i] = GetChoice(3); 
            
            // if enemy has 2 save points 
            if(result[0] == 'S' && Save+1 == 2 && i == 1)
            {
                result[i] = GetChoice(2); 
            }

            if (result[0] != 'S' && Save+1 == 2 && i == 1)
            {
                result[i] = GetChoice(3); 
            }

            // if enemy has 3 save points 
            if (Save +1 == 3)
            {
                result[i] = GetChoice(2); 
            }
        }

        enemyChoise = result; 
        return result; 


        char GetChoice(int max)
        {
            // max should not exceed 3; 
            int choice = random.Next(0,max); 

            switch(choice)
            {
                case 0: 
                return 'A'; 

                case 1: 
                return 'D'; 

                case 2: 
                return 'S'; 
            }

            return 'N';  // N for None 
        }

    }
}

