using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquationsOnConsoleGraph.Model; 

using static System.Console;
using static EquationsOnConsoleGraph.View.UIHelper; 

namespace EquationsOnConsoleGraph.View; 

internal static class MainView
{
    internal static string Invoke(GraphModel graphModel, StringBuilder message)
    {
        ClearConsoleView();
        WriteLine(); 

        if (message.Length > 0)
        {
            WriteLine($"{message.ToString()}"); 
        }

        return GetUserInput(); 
    }
}
