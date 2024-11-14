namespace Game; 

public class App
{
    public void Start()
    {
        bool quit = false; 

        while(!quit)
        {
            string userInput = UI.MainMenuOption(); 

            switch (userInput)
            {
                // send notification if player writes wrong words
                case "Quit": 
                quit = true; 
                break; 

                case "Play": 
                PlayArea(); 
                break; 
            }
        }
    }

    public void PlayArea()
    {
        bool quit = false; 

        while(!quit)
        {
            string userInput = ReadLine() ?? " "; 

            switch(userInput)
            {
                case "Quit": 
                quit = true; 
                break; 

                default: 
                WriteLine("Playing"); 
                break; 
            }

        }
    }
}