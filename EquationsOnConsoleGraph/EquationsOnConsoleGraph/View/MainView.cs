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
        WriteLine($"GraphProperties w:{graphModel.Width}, h:{graphModel.Height}");
        WriteLine($"{WindowHeight} {WindowWidth}"); 

        DrawGraph(graphModel); 
        

        return GetUserInput(); 
    }

    private static void DrawGraph(GraphModel graphModel)
    {
        for(int h = 0; h < graphModel.Height;  h++)
        {
            for (int w = 0; w < graphModel.Width; w++)
            {
                if (string.IsNullOrEmpty(graphModel.GraphPoints[h, w]))
                {
                    Write(" "); 
                }
                else
                {
                    Write(graphModel.GraphPoints[h, w]);
                }
            }
            WriteLine(); 
        }
    }
}
