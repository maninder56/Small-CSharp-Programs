using System.Collections;
using System.Globalization;

namespace Game; 
// Note: create a win logic 
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

    protected virtual void OnWrongInput(WrongInputEventArgs e) => WrongInput?.Invoke(this, e);
    
    protected virtual void OnChangeTurn(EventArgs e) => ChangeTurn?.Invoke(this, e); 
    

    protected virtual void OnGridFilled(EventArgs e) => GridFilled?.Invoke(this, e); 


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
            OnChangeTurn(EventArgs.Empty); 
            
            if (successfullTurns >= 9 && AreAllThePositionsFilled())
            {
                successfullTurns = 0; 
                OnGridFilled(EventArgs.Empty); 
            }
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
    }

    public bool AreAllThePositionsFilled()
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