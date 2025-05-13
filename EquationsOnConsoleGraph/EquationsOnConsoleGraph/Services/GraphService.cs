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

    private int zoomLevel = 1;

    private List<Equation> equations;

    // Number of spaces between 0 and 1 in X asix
    private readonly int spaceNumberOnXAxis;

    // Number of spaces between 0 and 1 in Y asix
    private const int spaceNumberOnYAxis = 5;

    public GraphService(int height, int width)
    {
        adjustGraphHeightForUserInput(ref height); 
        adjustGraphToCentreZero(ref height, ref width);

        graph = new GraphModel(height, width);

        spaceNumberOnXAxis = spaceNumberOnYAxis * 2;


        // only for testing 
        equations = new List<Equation>()
        {
            new Equation("x^2+2x-2"), 
            new Equation("x^3-3x^2-10-x"), 
            new Equation("x")
        }; 

        PlotXandYAxis(); 
    }

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
            graph.SetGraphPoint(verticalCentre + 1, w, $"{numbersOnPositiveXAxis++ * zoomLevel}");

        }

        // Plot negative x-axis points
        for (int w = horizontalCentre - spaceNumberOnXAxis; w > 0; w -= spaceNumberOnXAxis)
        {
            graph.SetGraphPoint(verticalCentre, w, ".");
            graph.SetGraphPoint(verticalCentre + 1, w, $"{numbersOnNegativeXAxis-- * zoomLevel}");
        }


        // Numbers to plot on Y axis 
        int numbersOnPositiveYAxis = 1;
        int numbersOnNegativeYAxis = -1;

        // Plot positive y-axis points
        for (int h = verticalCentre - spaceNumberOnYAxis; h > 0; h -= spaceNumberOnYAxis)
        {
            graph.SetGraphPoint(h, horizontalCentre, ".");
            graph.SetGraphPoint(h, horizontalCentre -1, $"{numbersOnPositiveYAxis++ * zoomLevel}"); 
        }

        // Plot negative y-axis points 
        for (int h = verticalCentre + spaceNumberOnYAxis; h < graph.Height; h += spaceNumberOnYAxis)
        {
            graph.SetGraphPoint(h, horizontalCentre, ".");
            graph.SetGraphPoint(h, horizontalCentre -1, $"{numbersOnNegativeYAxis-- * zoomLevel}");
        }

    }


    // Plot Equation
    private void PlotEquations()
    {
        // To get the center of x and y axis
        int verticalCentre = (graph.Height - 1) / 2;
        int horizontalCentre = (graph.Width - 1) / 2;

        // Numbers to plot on X axis
        int numbersOnPositiveXAxis = 1;
        int numbersOnNegativeXAxis = -1;


        double mainFunction(double x ) => (x-2) * (x+2)  * x;
        //double mainFunction(double x ) => (x*x) - (6*x) + 9;


        // Test Functions
        // f(x) = x
        //double function1(double x) => x;

        // f(x) = x^2
        //double function2(double x) => Math.Pow(x, 2);

        // f(x) = x^3
        //double function3(double x) => Math.Pow(x, 3);

        // Plot positive x-axis points
        for (int x = horizontalCentre; x < graph.Width; x++)
        {
            // compute accurate value of y 
            double accurateX = (double)numbersOnPositiveXAxis / (double)spaceNumberOnXAxis; 

            // accurateX will go into function to get y

            // Math.Pow = x^2 = f(x)

            double accurateY = equations[0].ComputY(accurateX) * (double)spaceNumberOnYAxis;
            numbersOnPositiveXAxis++;

            //if (accurateY % 1 != 0)
            //{
            //    continue; 
            //}

            int y = verticalCentre - (int)Math.Round(accurateY); 

            if (y < graph.Height && y > 0)
            {
                graph.SetGraphPoint(y, x, ".");
            }
        }

        // Plot negative x-axis points
        for (int x = horizontalCentre; x > 0; x--)
        {
            // compute accurate value of y 
            double accurateX = (double)numbersOnNegativeXAxis / (double)spaceNumberOnXAxis; 

            double accurateY = equations[0].ComputY(accurateX) * (double)spaceNumberOnYAxis;

            numbersOnNegativeXAxis--;

            //if (accurateY % 1 != 0)
            //{
            //    continue;
            //}

            int y = verticalCentre - (int)Math.Round(accurateY);

            if (y < graph.Height && y > 0)
            {
                graph.SetGraphPoint(y, x, ".");
            }
        }
    }


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
        PlotEquations();
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
    public GraphModel GetGraphModel()
    {
        // for testing only 
        PlotEquations();

        return graph;

    }


    



}
