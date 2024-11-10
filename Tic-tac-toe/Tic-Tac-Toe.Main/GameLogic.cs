using System.Collections;
using System.Globalization;

namespace Game; 
public class Grid 
{
    bool?[] boxes = new bool?[]
    {
        null, null, null,
        null, null, null,
        null, null, null
    }; 
    public bool?[] Boxes
    {
        get => boxes;
    }

    int successfullTurns = 0; 

    // Events 
    public event EventHandler<WrongInputEventArgs>? WrongInput; 
    public event EventHandler? ChangeTurn; 
    public event EventHandler? GridFilled; 
    public event EventHandler<PlayerWonEventArgs>? PlayerWon; 

    protected virtual void OnWrongInput(WrongInputEventArgs e) => WrongInput?.Invoke(this, e);
    protected virtual void OnChangeTurn(EventArgs e) => ChangeTurn?.Invoke(this, e); 
    protected virtual void OnGridFilled(EventArgs e) => GridFilled?.Invoke(this, e); 
    protected virtual void OnPlayerWon(PlayerWonEventArgs e) => PlayerWon?.Invoke(this, e); 

    
    public void UpdatePosition(string userInput, bool player)
    {
        bool isNumber = int.TryParse(userInput, out int position);

        if (!isNumber)
        {
            OnWrongInput(new WrongInputEventArgs("Please only Enter NUMBERS")); 
            return;
        }
        
        if (position < -1 || position > 8)
        {
            OnWrongInput(new WrongInputEventArgs("Only Numbers 0-8 are accepted.")); 
            return; 
        }

        if (boxes[position] != null)
        {
            OnWrongInput(new WrongInputEventArgs("Position has already been filled.")); 
            return; 
        }

        if (boxes[position] == null)
        {
            boxes[position] = player; 
            successfullTurns++; 
            
            if(successfullTurns >= 5 && HasPlayerWon(player))
            {
                OnPlayerWon(new PlayerWonEventArgs(player)); 
                return; 
            }

            // Win condition will be tested before braw condition
            if (successfullTurns >= 9 && AreAllThePositionsFilled())
            {
                OnGridFilled(EventArgs.Empty); 
                return; 
            }

            OnChangeTurn(EventArgs.Empty); 
        }
    }

    public void ResetGrid()
    {
        boxes = new bool?[]
        {
            null, null, null,
            null, null, null,
            null, null, null
        }; 
        successfullTurns = 0; 
    }

    bool AreAllThePositionsFilled()
    {
        foreach(bool? b in boxes)
        {
            if (b is null)
            {
                return false; 
            }
        }
        return true; 
    }

    bool HasPlayerWon(bool player)
    {
        int[,] possibleWinCombinations = new int[,] 
        {  
            { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 },
            { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 },
            { 0, 4, 8 }, { 2, 4, 6 }
        };  

        for (int winboxes=0; winboxes < 8; winboxes++)
        {
            int[] positionNumbers = new int[3]; 

            for(int position=0; position < 3; position++)
            {
                positionNumbers[position] = possibleWinCombinations[winboxes, position]; 
            }

            if (checkPlayerWon(positionNumbers))
            {
                return true; 
            }            
        }

        return false; 


        bool checkPlayerWon(int[] positionsNumber)
        {
            if (positionsNumber.Length > 3)
            {
                throw new ArgumentOutOfRangeException("Can not have more than 3 positions"); 
            }

            bool[] positions = new bool[3]; 

            for(int i =0; i < 3; i++)
            {
                if (boxes[positionsNumber[i]] is bool p)
                {
                    positions[i] = p; 
                }

                if (boxes[positionsNumber[i]] is null)
                {
                    return false; 
                }
            }

            if (player)
            {
                return positions[0] & positions[1] & positions[2]; 
            }

            return !(positions[0] | positions[1] | positions[2]); 
        }

    }

    public Grid()
    {
        // Assign Wrong Input Events 
        WrongInput += (object? sender, WrongInputEventArgs e) => 
        {
            Messages.WriteBadInputMessage(e.message); 
            WriteLine("Press Enter"); 
            ReadLine(); 
        }; 
    }

}



// Notification Info sent by Grid Class to user for wrong input 
public class WrongInputEventArgs : EventArgs
{
    public string message; 
    
    public WrongInputEventArgs(string message) => this.message = message; 
}

public class PlayerWonEventArgs : EventArgs
{
    public bool player; 

    public PlayerWonEventArgs(bool player) => this.player = player; 
}


public class Score
{
    public int player1Score {get; set; } = 0; 
    public int player2Score {get; set; } = 0; 
    public int draw {get; set; } = 0;
}