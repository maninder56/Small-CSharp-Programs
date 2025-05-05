using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationsOnConsoleGraph.Model; 

class GraphModel
{
    public int Width { get; private set; }

    public int Height { get; private set; }

    public string[,] GraphPoints { get; private set; }

    public GraphModel(int height, int width)
    {
        Height = height;
        Width = width;
        GraphPoints = new string[Height, Width]; 
    }

    public void SetWidth(int width) => width = Width;

    public void SetHeight(int height) => height = Height;

    public void ResetGraphPoints()
    {
        GraphPoints = new string[Height, Width];
    }

    public void SetGraphPoint(int y, int x, string mark) => GraphPoints[y, x] = mark;
}
