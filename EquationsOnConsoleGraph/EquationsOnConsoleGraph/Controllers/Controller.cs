using EquationsOnConsoleGraph.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationsOnConsoleGraph.Controllers; 

class Controller
{
    StringBuilder message = new StringBuilder();

    public Controller () { }

    public bool Invoke()
    {
        string userInput = MainView.Invoke(message);
        message.Clear();

        switch(userInput)
        {
            case "Quit":
                return true;

            case "Help":
            case "help":
                return false;

            default:
                return false;
        }
    }
}
