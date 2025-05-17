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
    internal static string Invoke(GraphModel graphModel, List<Equation> equationList)
    {
        ClearConsoleView();

        DrawGraph(graphModel);

        WriteEquationList(equationList); 
        
        WriteLine(new string('-', WindowWidth)); 

        if (Message.MessagePending)
        {
            WriteLineWithColor(Message.Color, Message.Content.ToString());
            Message.ClearMessage();
        }

        WriteLine("Draw Eqation"); 
        return GetUserInput(); 
    }

    private static void DrawGraph(GraphModel graphModel)
    {
        // extra space needed to center the graph
        int extraSpace = 3; 

        for(int h = 0; h < graphModel.Height;  h++)
        {
            //Thread.Sleep(100);
            Write(new string(' ', extraSpace));
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

    private static void WriteEquationList(List<Equation> equationList)
    {
        WriteLine("Number of Equations: {0}", equationList.Count);

        int count = 1; 

        foreach(Equation equation in equationList)
        {
            WriteLine($"Equation {count++} : {equation.rawEquation}");
        }

    }
}
