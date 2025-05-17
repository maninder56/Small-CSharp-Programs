using EquationsOnConsoleGraph.Model;
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

    public Controller (GraphService graphService) 
    { 
        this.graphService = graphService;
    }

    public bool Invoke()
    {
        graphService.UpdateDimentionsIfChanged(); 

        //graphService.TestPlotAllEquations();

        string userInput = MainView.Invoke(graphService.GetGraphModel());

        switch(userInput)
        {
            case "Quit":
                return true;

            case "Help":
            case "h":
                HelpView.Invoke(); 
                return false;

            case "zi":
            case "ZoomIn":
                ZoomInHandler();
                return false;

            case "zo":
            case "ZoomOut":
                ZoomOutHandler(); 
                return false;

            case "zd":
            case "ZoomDefault": 
                SetZoomToDefaultHandler();
                return false;

            case "Clear": 
                ClearAllEquationsHandler();
                return false;

            default:
                Message.AddMessage(MessageType.Warning, "Incorrect Command"); 
                return false;
        }
    }


    // Command handlers

    // ZoomIN 
    private void ZoomInHandler()
    {
        graphService.IncreasZoomLevel(out string zoomMessage);

        if(!string.IsNullOrEmpty(zoomMessage))
        {
            Message.AddMessage(MessageType.Warning, zoomMessage);
        }
    }

    private void ZoomOutHandler()
    {
        graphService.DecreaseZoomLevel(out string zoomMessage);

        if (!string.IsNullOrEmpty(zoomMessage))
        {
            Message.AddMessage(MessageType.Warning, zoomMessage);
        }
    }

    private void SetZoomToDefaultHandler() => graphService.SetZoomLevelToDefault();

    private void ClearAllEquationsHandler() => graphService.ClearGraphAndEquations(); 



}
