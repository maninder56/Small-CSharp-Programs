namespace Game; 

public class App 
{
    // Add a Scoring System to tell the user final score
    // add fields to store score
    public void Start()
    {
        bool quit = false; 
        while(!quit)
        {
            // string userInput = UI.MainMenuOption(); 

            // Temporary variable
            string userInput = "Play";

            switch (userInput)
            {
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
        Grid grid = new Grid(); 

        while(!quit)
        {
            string userInput = UI.MainGameLoop(grid); 

            switch(userInput)
            {
                case "Quit": 
                quit = true; 
                break; 

                default: 
                grid.UpdatePosition(Int32.Parse(userInput)); 
                break; 
            }

        } 

        
    }
}   