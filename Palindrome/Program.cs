using System;
using static System.Console;

using System.Text;
using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        WriteLine();

        runPalindrome();
    }

    static void runPalindrome()
    {
        while(true)
        {
            WriteLine("Type a Word:");
            string input = ReadLine();


            // replace all the punctuation or spaces and convert all the char to lower letters
            input = input.Replace(" ", string.Empty);
            input = Regex.Replace(input,"[!\"#$%&'()*+,-./:;<=>?@\\[\\]^_`{|}~]", string.Empty);
            input = input.ToLower();

            bool palindrome = isPalindrome(input); 

            if (palindrome)
            {
                WriteLine($"Palindrome: {palindrome}, Length: {input.Length}\n");
            }
            else 
            {
                WriteLine("Palindrome: False");
            }
            
        }
        
    }

    static bool isPalindrome(string input)
    {
        // compare the first and last char and 
        // return true or false based on Match. 

        string localInput = input; 

        if (localInput == string.Empty) // if string is Empty 
        {
            WriteLine("Please Try again");
            return false;
        }

        int loop = 0; 
        if (localInput.Length % 2 == 0) // length of string is even 
        {
            loop = localInput.Length / 2; 
        }
        else 
        {
            loop = localInput.Length - 1; // to ignore middle char
        }
        

        for (int i = 0; i < loop; i++)
        {
            //WriteLine($"{localInput[i]},  {localInput[localInput.Length - (i+1)]}");

            // compare first and last char 
            if (localInput[i] != localInput[localInput.Length - (i+1)])
            {
                return false;
            }
        }

        return true;
    }
}

