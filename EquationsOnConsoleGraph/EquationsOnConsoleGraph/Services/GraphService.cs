using EquationsOnConsoleGraph.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EquationsOnConsoleGraph.Services; 

class GraphService
{
    private GraphModel graph;

    private int zoomLevel = 1; 

    public GraphService(int height, int width)
    {
        adjustGraphHeightForUserInput(ref height); 
        adjustGraphToCentreZero(ref height, ref width);

        graph = new GraphModel(height, width);

        PlotXandYAxis(); 
    }

    // Helper Methods 

    // To decrease height to make space for user Input
    private void adjustGraphHeightForUserInput(ref int height) => height = height - 8; 

    // To decrease width to position zero in center
    private void adjustGraphToCentreZero(ref int height, ref int width)
    {
        height = height % 2 == 0 ? height -= 5 : height -= 4;
        width = width % 2 == 0 ? width -= 5 : width -= 4;
    }

    // Plot x and y axis 
    private void PlotXandYAxis()
    {
        int verticalCentre = (graph.Height - 1) / 2;
        int horizontalCentre = (graph.Width - 1) / 2;

        int numbersOnXAxisAfterZero = 1;
        int numbersOnXAxisBeforeZero = -1;

        graph.SetGraphPoint(verticalCentre, horizontalCentre, ".");
        graph.SetGraphPoint(verticalCentre + 1, horizontalCentre, "0");

        // Plot x-axis points after zero
        for (int w = horizontalCentre + 10; w < graph.Width; w += 10)
        {
            graph.SetGraphPoint(verticalCentre, w, ".");
            graph.SetGraphPoint(verticalCentre + 1, w, $"{numbersOnXAxisAfterZero++}");

        }

        // Plot x-axis points before zero
        for (int w = horizontalCentre - 10; w > 0; w -= 10)
        {
            graph.SetGraphPoint(verticalCentre, w, ".");
            graph.SetGraphPoint(verticalCentre + 1, w, $"{numbersOnXAxisBeforeZero--}");
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

        // need to re draw equations after reseting graph
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




    



}
