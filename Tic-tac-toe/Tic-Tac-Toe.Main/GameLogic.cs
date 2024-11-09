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

    // Events 
    public event EventHandler<WrongInputEventArgs>? WrongInput; 

    protected virtual void OnWrongInput(WrongInputEventArgs e)
    {
        WrongInput?.Invoke(this, e);
    }

    public void UpdatePosition(int position)
    {
        if (position < -1 || position > 9)
        {
            return; 
        }

        if (boxes[position] == null)
        {
            boxes[position] = true; 
        }
    }

    public Grid()
    {
        // Assign Wrong Input Events 
        WrongInput += (object? sender, WrongInputEventArgs e) => Messages.WriteBadInputMessage(e.message); 
    }

}



// Notification Info sent by Grid Class to user for wrong input 
public class WrongInputEventArgs : EventArgs
{
    public string message; 
    
    public WrongInputEventArgs(string message) => this.message = message; 
}