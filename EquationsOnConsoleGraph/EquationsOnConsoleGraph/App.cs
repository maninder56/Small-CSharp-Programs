using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Quic;
using System.Text;
using System.Threading.Tasks;
using EquationsOnConsoleGraph.Controllers;
using EquationsOnConsoleGraph.Services;

namespace EquationsOnConsoleGraph; 

class App
{
    private bool quit = false;

    // Application's window height and width 
    int windowHeight = Console.WindowHeight;
    int windowWidth = Console.WindowWidth;

    GraphService graphService; 

    Controller controller;
    
    public App()
    {
        graphService = new GraphService(windowHeight, windowWidth);
        controller = new Controller(graphService);
    }

    public void Start()
    {
        do
        {
            quit = controller.Invoke();

            // Check if console window dimentions have changed
            int currentWindowHeight = Console.WindowHeight; 
            int currentWindowWidth = Console.WindowWidth;

            if (windowHeight != currentWindowHeight || windowWidth != currentWindowWidth)
            {
                graphService.UpdateGraphInfo(height: currentWindowHeight, width: currentWindowWidth);
            }

        } while(!quit);  
    }

}
