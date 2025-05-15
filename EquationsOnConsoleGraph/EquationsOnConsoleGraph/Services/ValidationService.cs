using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationsOnConsoleGraph.Services; 

public static class ValidationService
{
    // Helper methods 
    private static List<string> GetEquationTerms(string equation)
    {
        List<string> terms = new List<string>();

        StringBuilder term = new StringBuilder();

        for (int i = 0; i < equation.Length; i++)
        {
            if (equation[i] == '-' || equation[i] == '+')
            {
                string currentTerm = term.ToString().Trim();

                if (!string.IsNullOrEmpty(currentTerm))
                {
                    terms.Add(currentTerm);
                }
                term.Clear();
            }

            if (!(equation[i..].Contains('+') || equation[i..].Contains('-')))
            {
                if (i - 1 == -1)
                {
                    terms.Add(equation[i..]);
                    break;
                }

                terms.Add(equation[(i - 1)..]);
                break;
            }

            term.Append(equation[i]);
        }

        return terms;
    }


    private static bool ValidateTerm(string term, out string message)
    {
        message = string.Empty;
        // remove term sign
        if (term[0] == '-' || term[0] == '+')
        {
            term = term[1..];
        }

        if (term.Contains('-') || term.Contains('+'))
        {
            message = $"Invalid term : {term}, term only has just sign or more then one"; 
            return false;
        }

        // If term does not contains x then it must be a number
        if (!term.Contains('x'))
        {
            return double.TryParse(term, out _);
        }

        // If x is the only letter 
        if (term.Contains('x') && term.Length == 1)
        {
            return true;
        }

        // if term starts with x and has power
        if (term.StartsWith("x^"))
        {
            bool isPower = double.TryParse(term[(term.IndexOf('^') + 1)..], out double power);

            if (isPower)
            {
                return power > 0.0;
            }

            return false;
        }

        // when term has number before x with power
        if (term.Contains("x^"))
        {
            bool isNumber = double.TryParse(term[..term.IndexOf('x')], out _);

            bool isPower = double.TryParse(term[(term.IndexOf('^') + 1)..], out double power);

            if (isPower)
            {
                return power > 0.0 && isNumber;
            }

            return false;
        }

        // When term contains x with no power
        // any number multiplies x can only come before x
        if (term.Contains('x'))
        {
            // anything after x 
            if (!string.IsNullOrEmpty(term[(term.IndexOf('x') + 1)..]))
            {
                return false;
            }

            return double.TryParse(term[..term.IndexOf('x')], out _);
        }

        return false;
    }






    public static bool ValidateEquation(string equation, out string message)
    {
        message = string.Empty;

        if (equation.Contains("--") || equation.Contains("++"))
        {
            message = "Each term can only have one + or - sign";
            return false; 
        }

        if (equation.EndsWith('-'))
        {
            message = "Eqation can not have negative sign at the end"; 
            return false;
        }

        List<string> terms = GetEquationTerms(equation);

        foreach(string term in terms)
        {
            if (!ValidateTerm(term))
            {
                return false; 
            }
        }
        return true;
    }
}
