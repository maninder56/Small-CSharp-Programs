using EquationsOnConsoleGraph.Model;
using EquationsOnConsoleGraph.Services;
using EquationsOnConsoleGraph.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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

        string userInput = MainView.Invoke(graphService.GetGraphModel(), graphService.GetEquationList());

        string firstWord = getFirstWord(userInput);

        switch(firstWord)
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

            case "E":
                DrawEquationHandler(userInput);
                return false;

            default:
                Message.AddMessage(MessageType.Warning, "Incorrect Command"); 
                return false;
        }
    }


    // Command handlers

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


    private void DrawEquationHandler(string userInput)
    {
        // remove command word 
        string equation = removeFirstWord(userInput);

        if (string.IsNullOrEmpty(equation))
        {
            Message.AddMessage(MessageType.Warning, "No Eqation Given"); 
            return; 
        }

        // Validate Equation
        if (!ValidationService.ValidateEquation(equation, out string validationMessage))
        {
            Message.AddMessage(MessageType.Warning, validationMessage);
            return; 
        }

        graphService.AddAndPlotEquation(equation, out string addEquationMessage); 

        if (!string.IsNullOrEmpty(addEquationMessage))
        {
            Message.AddMessage(MessageType.Warning, addEquationMessage);
            return; 
        }
    }


    private string removeFirstWord(string userInput)
    {
        if (userInput.Length > 2)
        {
            return userInput.Remove(0, 2);
        }

        return string.Empty;
    }


    private string getFirstWord(string userInput)
    {
        if (userInput.IndexOf(' ') != -1)
        {
            return userInput.Substring(0, userInput.IndexOf(' '));
        }

        return userInput;
    }

}
