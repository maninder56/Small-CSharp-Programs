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
            message = $"Invalid Equation only one negative or positive sign is allowed"; 
            return false;
        }

        // If term does not contains x then it must be a number
        if (!term.Contains('x'))
        {
            if (double.TryParse(term, out _))
            {
                message = $"{term} is not allowed"; 
                return true; 
            }

            return false;
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
                if (power > 0.0)
                {
                    return true; 
                }
                message = $"{term} has negative power"; 
                return false; 
            }

            message = $"{term} is not allowed";
            return false;
        }

        // when term has number before x with power
        if (term.Contains("x^"))
        {
            bool isNumber = double.TryParse(term[..term.IndexOf('x')], out _);

            bool isPower = double.TryParse(term[(term.IndexOf('^') + 1)..], out double power);

            if (isPower)
            {
                if (power > 0.0 && isNumber)
                {
                    return true; 
                }

                message = $"{term} has negative power or number before x is invalid";
                return false; 
            }

            message = $"{term} is not allowed";
            return false;
        }

        // When term contains x with no power
        // any number multiplies x can only come before x
        if (term.Contains('x'))
        {
            // anything after x 
            if (!string.IsNullOrEmpty(term[(term.IndexOf('x') + 1)..]))
            {
                message = $"Numbers to be multiplied by x are only allowed before x"; 
                return false;
            }

            if (double.TryParse(term[..term.IndexOf('x')], out _))
            {
                return true; 
            }

            message = $"{term} only numbers are allowed before x"; 
            return false; 

        }

        message = $"{term} is not Invalid";
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
            if (!ValidateTerm(term, out string termValidationMessage))
            {
                message = termValidationMessage; 
                return false; 
            }
        }
        return true;
    }
}
