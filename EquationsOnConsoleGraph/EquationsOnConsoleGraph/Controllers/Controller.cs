using EquationsOnConsoleGraph.Services;
using EquationsOnConsoleGraph.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationsOnConsoleGraph.Controllers; 

class Controller
{
    GraphService graphService; 

    // need to create message class to incoporate type of message 
    StringBuilder message = new StringBuilder();

    public Controller (GraphService graphService) 
    { 
        this.graphService = graphService;
    }

    public bool Invoke()
    {
        // Check if console window dimentions have changed
        //int currentWindowHeight = Console.WindowHeight;
        //int currentWindowWidth = Console.WindowWidth;

        //if (graphService.GetGraphHeight() != currentWindowHeight || graphService.GetGraphWidth() != currentWindowWidth)
        //{
        //    graphService.UpdateGraphInfo(height: currentWindowHeight, width: currentWindowWidth);
        //}

        string userInput = MainView.Invoke(graphService.GetNewGraphModel(), message);
        message.Clear();

        switch(userInput)
        {
            case "Quit":
                return true;

            case "Help":
            case "help":
                return false;

            default:
                return false;
        }
    }
}
