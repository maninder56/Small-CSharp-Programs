using EquationsOnConsoleGraph.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EquationsOnConsoleGraph.Services; 

class GraphService
{
    private GraphModel graph;

    private List<Equation> equationList = new List<Equation>()
    {
        // only for tests 
        new Equation("x^2+2x-2"),
        new Equation("x^3-x^2-x")
    }; 

    // Number of spaces between 0 and 1 in X asix
    private readonly int spaceNumberOnXAxis;

    // Number of spaces between 0 and 1 in Y asix
    private int spaceNumberOnYAxis = 10;


    public GraphService(int height, int width)
    {
        adjustGraphHeightForUserInput(ref height); 
        adjustGraphToCentreZero(ref height, ref width);

        graph = new GraphModel(height, width);

        spaceNumberOnXAxis = spaceNumberOnYAxis * 2;

        PlotXandYAxis(); 
    }


    #region Helper Methods
    // Helper Methods 

    // To decrease height to make space for user Input
    private void adjustGraphHeightForUserInput(ref int height) => height -= 4; 

    // To decrease width to position zero in center
    private void adjustGraphToCentreZero(ref int height, ref int width)
    {
        height = height % 2 == 0 ? height -= 5 : height -= 4;
        width = width % 2 == 0 ? width -= 7 : width -= 6;
    }

    // Plot x and y axis 
    private void PlotXandYAxis()
    {
        // To get the center of x and y axis
        int verticalCentre = (graph.Height - 1) / 2;
        int horizontalCentre = (graph.Width - 1) / 2;
        

        // Set zero in the center of graph
        graph.SetGraphPoint(verticalCentre, horizontalCentre, ".");
        graph.SetGraphPoint(verticalCentre + 1, horizontalCentre, "0");

        // Numbers to plot on X axis
        int numbersOnPositiveXAxis = 1;
        int numbersOnNegativeXAxis = -1;
        
        // Plot positive x-axis points
        for (int w = horizontalCentre + spaceNumberOnXAxis; w < graph.Width; w += spaceNumberOnXAxis)
        {
            graph.SetGraphPoint(verticalCentre, w, ".");
            graph.SetGraphPoint(verticalCentre + 1, w, $"{numbersOnPositiveXAxis++}");

        }

        // Plot negative x-axis points
        for (int w = horizontalCentre - spaceNumberOnXAxis; w > 0; w -= spaceNumberOnXAxis)
        {
            graph.SetGraphPoint(verticalCentre, w, ".");
            graph.SetGraphPoint(verticalCentre + 1, w, $"{numbersOnNegativeXAxis--}");
        }


        // Numbers to plot on Y axis 
        int numbersOnPositiveYAxis = 1;
        int numbersOnNegativeYAxis = -1;

        // Plot positive y-axis points
        for (int h = verticalCentre - spaceNumberOnYAxis; h > 0; h -= spaceNumberOnYAxis)
        {
            graph.SetGraphPoint(h, horizontalCentre, ".");
            graph.SetGraphPoint(h, horizontalCentre -1, $"{numbersOnPositiveYAxis++}"); 
        }

        // Plot negative y-axis points 
        for (int h = verticalCentre + spaceNumberOnYAxis; h < graph.Height; h += spaceNumberOnYAxis)
        {
            graph.SetGraphPoint(h, horizontalCentre, ".");
            graph.SetGraphPoint(h, horizontalCentre -1, $"{numbersOnNegativeYAxis--}");
        }

    }


    // Plot Equation
    private void PlotEquation(Equation equation)
    {
        // To get the center of x and y axis
        int verticalCentre = (graph.Height - 1) / 2;
        int horizontalCentre = (graph.Width - 1) / 2;

        // Numbers to plot on X axis
        int numbersOnPositiveXAxis = 1;
        int numbersOnNegativeXAxis = -1;

        // Plot positive x-axis points
        for (int x = horizontalCentre; x < graph.Width; x++)
        {
            // x represents actual space of grid 
            // accurateX and accurateY represents (x,y) adjusted to space

            // compute accurate value of y 
            double accurateX = (double)numbersOnPositiveXAxis / (double)spaceNumberOnXAxis; 

            // accurateX will go into function to get y
            double accurateY = equation.ComputY(accurateX) * (double)spaceNumberOnYAxis;
            numbersOnPositiveXAxis++;


            int y = verticalCentre - (int)Math.Round(accurateY); 

            if (y < graph.Height && y > 0)
            {
                if (!string.IsNullOrEmpty(graph.GetGraphPoint(y, x)))
                {
                    continue; 
                }

                graph.SetGraphPoint(y, x, ".");
            }
        }

        // Plot negative x-axis points
        for (int x = horizontalCentre; x > 0; x--)
        {
            // compute accurate value of y 
            double accurateX = (double)numbersOnNegativeXAxis / (double)spaceNumberOnXAxis; 

            double accurateY = equation.ComputY(accurateX) * (double)spaceNumberOnYAxis;

            numbersOnNegativeXAxis--;

            int y = verticalCentre - (int)Math.Round(accurateY);

            if (y < graph.Height && y > 0)
            {
                if (!string.IsNullOrEmpty(graph.GetGraphPoint(y, x)))
                {
                    continue;
                }
                    
                graph.SetGraphPoint(y, x, ".");
            }
        }
    }


    // Plot All Available Equations 
    private void PlotAllEquations()
    {
        if (equationList.Count > 0)
        {
            foreach (Equation equation in equationList)
            {
                PlotEquation(equation);
            }
        }
    }

    #endregion


    #region Public Methods
    // To get heigh and width of graph
    public int GetGraphHeight() => graph.Height;
    public int GetGraphWidth() => graph.Width;

    // Update graph dimentions 
    public void UpdateGraphInfo(int height, int width)
    {
        graph.SetHeight(height);
        graph.SetWidth(width);
        graph.ResetGraphPoints();

        PlotXandYAxis(); 
        PlotAllEquations();
    }

    // Check if console window dimentions have changed
    public void UpdateDimentionsIfChanged()
    {
        int currentWindowHeight = Console.WindowHeight;
        int currentWindowWidth = Console.WindowWidth;

        adjustGraphHeightForUserInput(ref currentWindowHeight);
        adjustGraphToCentreZero(ref currentWindowHeight, ref currentWindowWidth);

        if (graph.Height != currentWindowHeight || graph.Width != currentWindowWidth)
        {
            UpdateGraphInfo(height: currentWindowHeight, width: currentWindowWidth);
        }
    }


    // Clear all points and set x and y asix 
    public void ClearGraph()
    {
        graph.ResetGraphPoints();

        PlotXandYAxis(); 
    }

    // Get Graph model 
    public GraphModel GetGraphModel() => graph;

    // Add Equation to equationList and plot given equation
    public void AddAndPlotEquation(Equation equation)
    {
        equationList.Add(equation);
        PlotEquation(equation);
    }

    public void TestPlotAllEquations() => PlotAllEquations();// only for tests 


    // Set the zoom level to default 
    // zoom level represents number of spaces between 0 and 1 
    public void SetZoomLevelToDefault() => spaceNumberOnYAxis = 10;

    // Increase zoom level
    public void IncreasZoomLevel()
    {
        if (spaceNumberOnYAxis < 20)
        {
            spaceNumberOnYAxis += 5;
        }
    }

    // Decrease zoom level 
    public void DecreaseZoomLevel()
    {
        if (spaceNumberOnYAxis > 5)
        {
            spaceNumberOnYAxis -= 5;
        }
    }

    #endregion

}
