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
    }

    // Helper Methods 

    // To decrease height to make space for user Input
    private int adjustGraphHeightForUserInput(ref int height) => height - 3; 

    // To decrease width to position zero in center
    private void adjustGraphToCentreZero(ref int height, ref int width)
    {
        height = height % 2 == 0 ? height -= 3 : height -= 2;
        width = width % 2 == 0 ? width -= 3 : width -= 2;
    }


    public void UpdateGraphInfo(int height, int width)
    {
        adjustGraphHeightForUserInput(ref height);
        adjustGraphToCentreZero(ref height, ref width);

        graph.Height = height;
        graph.Width = width;
        graph.GraphPoints = new string[height, width];
    }

    public GraphModel GetNewGraphModel()
    {
        graph.ResetGraphPoints();

        // Plot x-axis
        

        return graph;
    }


}
