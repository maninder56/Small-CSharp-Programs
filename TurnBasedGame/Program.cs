global using System; 
global using static System.Console;
using System.Security.Cryptography.X509Certificates;
using Game; 

internal class Program
{
    private static void Main(string[] args)
    {
        App app = new App(); 
        //app.Start(); 

        Random random = new Random(); 


        char GetEnemyMove()
        {
            int move = random.Next(0,2); 
            Write(move + "  "); 

            switch (move)
            {
                case 0: 
                return 'A'; 

                default: 
                return 'D'; 
            }

        }

        for (int i=0; i < 10; i++)
        {
            WriteLine(GetEnemyMove()); 
        }

    
    

    }
}