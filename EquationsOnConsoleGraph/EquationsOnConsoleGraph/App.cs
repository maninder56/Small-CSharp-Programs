using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Quic;
using System.Text;
using System.Threading.Tasks;
using EquationsOnConsoleGraph.Controllers; 

namespace EquationsOnConsoleGraph; 

class App
{
    private bool quit = false; 

    Controller controller;
    
    public App()
    {
        controller = new Controller();
    }

    public void Start()
    {
        do
        {
            quit = controller.Invoke();
        } while(!quit);  
    }

}
