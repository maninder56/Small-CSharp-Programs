namespace Application; 

public class App 
{
    public void Start()
    {
        while(true)
        {
            string userInput = UI.MainMenuOption(); 

            switch (userInput)
            {
                case "Quit": 
                return; 

                case "Play": 
                WriteLine("You are playing"); 
                ReadLine(); 
                break; 
            }
        }
    }
}