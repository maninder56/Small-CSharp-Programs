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
        WriteLine($"Raw w:{WindowWidth}, h:{WindowHeight}");

        DrawGraph(graphModel); 
        

        return GetUserInput(); 
    }

    private static void DrawGraph(GraphModel graphModel)
    {
        for(int h = 0; h < graphModel.Height;  h++)
        {
            for (int w = 0; w < graphModel.Width; w++)
            {
                string currentSymbol = graphModel.GraphPoints[h, w];


                if (string.IsNullOrEmpty(currentSymbol))
                {
                    Write(" ");
                    continue;
                }

                if (currentSymbol.Length == 1)
                {
                    Write(currentSymbol);
                    continue; 
                }

                if (currentSymbol.Length > 1)
                {
                    int numberToAdjustGraph = currentSymbol.Length - 1;

                    Write(new string('\b', numberToAdjustGraph));
                    Write(currentSymbol); 
                    continue;
                }

            }
            WriteLine(); 
        }
    }
}
