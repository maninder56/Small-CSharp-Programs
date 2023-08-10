using System;
using static System.Console;

internal class Program
{
    private static void Main(string[] args)
    {
        WriteLine();

        rungame();
    }

    static void rungame()
    {
        int theNumber = new Random().Next(20);

        WriteLine("Let's Play 'Guess the Number'!");
        WriteLine("I'm thinking of a number between 0 and 20.");
        WriteLine("Enter your guess, or -1 to give up.");

        int input;
        bool guessIsRight = false;
        int count = 0;
        do
        {
            bool isNumber = int.TryParse(ReadLine(), out input);

            if (isNumber)
            {
                guessIsRight = rightGuess(theNumber, input);
                count++;
            }
            else 
            {
                WriteLine("It is not a number");
            }
        }
        while(input != -1 && guessIsRight != true);



        if (guessIsRight)
        {
            WriteLine($"That is the right !!!, you got it in {count} tries.");
        }
        else
        {
            WriteLine("Better Luck next Time :)");
        }
        

    }

    static bool rightGuess(int theNumber, int guess)
    {
        int result = theNumber.CompareTo(guess);

        bool guessIsRight = false;

        switch(result)
        {
            case -1:
                WriteLine("Nope, Lower than that.");
                break;

            case 1:
                WriteLine("Nope, Higher than that.");
                break;

            case 0:
                guessIsRight = true;
                break;
        }

        return guessIsRight;
    }
}