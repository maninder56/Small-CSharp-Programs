using System;
using System.Net;
using static System.Console;

internal class Program
{
    private static void Main(string[] args)
    {
        WriteLine(BinomialExpansion(new int[] { 2, -3, 6}));
    }

    // Method takes array of simplified memebers of expression 
    // each index respresenting a,bx, and n. (n represents power)
    // n has to be positive whole number 
    static string BinomialExpansion(int[] expression)
    {
        string expantion = string.Empty;

        for (int term = 1; term < (expression[2] + 1); term++)
        {
           expantion += $"{TermInExpansion(term,expression[0],expression[1],expression[2])}, ";
        }

        return expantion;
    }

    // returns factorial of given number
    static int Factorial (int number)
    {
        int factorial = number;
        while (number > 1)
        {
            number--;
            factorial *= number;
        }
        return factorial;
    }

    // returns a given term in expantion
    // term needs to be bigger then zero and smaller then n
    // using General formula for expanding (a+b)^n
    static string TermInExpansion(int termNumber, int a, int b, int n)
    {
        string term; 
        int r = termNumber - 1; // power of x 

        if (termNumber == 1) // if it's first term 
        {
            term = $"{Math.Pow(a,n)}";
            return term;
        }

        if (termNumber == n) // if it's last term
        {
            term = $"{NCrFormula(n,r) * Math.Pow(a,n-r) * Math.Pow(b,r)}x^{r}, {Math.Pow(b,n)}x^{r+1}";
            return term;
        }

        // for middle term
        term = $"{NCrFormula(n,r) * Math.Pow(a,n-r) * Math.Pow(b,r)}x^{r}";

        return term;
    }

    // returns part of coefficient of term using nCr formula
    static int NCrFormula (int n, int r)
    {
        int coefficient = Factorial(n) / (Factorial(r) * Factorial(n - r));
        return coefficient;
    }


    
}