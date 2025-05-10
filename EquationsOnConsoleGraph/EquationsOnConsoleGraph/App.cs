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
        graphService = new GraphService(height:windowHeight, width: windowWidth);
        controller = new Controller(graphService);
    }

    public void Start()
    {
        do
        {
            quit = controller.Invoke();
            quit = true; // For testing only

        } while (!quit);  
    }

}
