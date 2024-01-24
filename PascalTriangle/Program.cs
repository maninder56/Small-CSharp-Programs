using System;
using static System.Console;

internal class Program
{
    private static void Main(string[] args)
    {
        PascalTriangle(5);
    }

    // print Pascal triangle upto given number 
    static void PascalTriangle(int rowNumber)
    {
        // add assured first two terms 
        WriteLine("1\n1  1");

        for (int i = 2; i < (rowNumber + 1); i++)
        {
            string currentRow = PascalRow(i);
            WriteLine(currentRow);
        }
    }

    // return factorial of given number 
    static int Factorial(int number)
    {
        int result = number;

        while (number > 1)
        {
            number--;
            result = number * result;
        }

        return result;
    }

    // return Pascal row in string 
    static string PascalRow(int rowNumber)
    {
        // add first assured number of row 
        string result = $"1 ";

        // calculate middle terms 
        for (int i = 1; i < rowNumber; i++)
        {
           int currentTurmNumber = Factorial(rowNumber) / (Factorial(i) * Factorial(rowNumber - i));
           result += $" {currentTurmNumber} ";
        }

        // add last assured number of row 
        result += $" 1";

        return result;
    }
}