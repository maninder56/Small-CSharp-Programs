using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Console;
using static EquationsOnConsoleGraph.View.UIHelper; 

namespace EquationsOnConsoleGraph.View; 

public static class HelpView
{
    public static void Invoke()
    {
        ClearConsoleView();
        WriteLine();

        WriteLine("""
            Commands : 

            Quit            - To Quit
            Help | h        - To open help 

            [Eqation]       - To Draw an eqation, only three equations at a time are allowed

            ZoomIn | zi     - To zoom in on a graph 
            ZoomOut | zo    - To zoom out on a graph

            Clear           - To remove all the eqations from the graph

            """);

        WriteLine("Press enter to back to main menu"); 
        GetUserInput();
    }
}
