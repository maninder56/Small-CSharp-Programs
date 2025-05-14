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
        graphService.UpdateDimentionsIfChanged(); 

        graphService.TestPlotAllEquations();

        string userInput = MainView.Invoke(graphService.GetGraphModel(), message);
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
