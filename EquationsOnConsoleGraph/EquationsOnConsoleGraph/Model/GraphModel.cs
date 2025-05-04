using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationsOnConsoleGraph.Model; 

class GraphModel
{
    public int Width { get; set; }

    public int Height { get; set; }

    public string[,] GraphPoints { get; set; }

    public GraphModel(int height, int width)
    {
        Height = height;
        Width = width;
        GraphPoints = new string[Height, Width]; 
    }

    public void ResetGraphPoints()
    {
        GraphPoints = new string[Height, Width];
    }
}
