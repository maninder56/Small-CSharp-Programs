using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationsOnConsoleGraph.Model; 

public class Equation
{
    // Add properties to store eqation roots and when equation hits y axis

    List<string> terms; 

    public Equation(string equation)
    {
        terms = GetEquationTerms(equation); 
    }

    // Split the equations in terms 
    private List<string> GetEquationTerms(string equation)
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


    // To compute each term
    private double computeTerm(string term, double x)
    {
        double result = 0.0;

        if (string.IsNullOrEmpty(term))
        {
            return 0;
        }

        // check if term is negative or positive
        bool isPositive = term[0] != '-';

        if (term.Contains('-'))
        {
            term = term.Remove(term.IndexOf('-'), 1);
        }

        if (term.Contains('+'))
        {
            term = term.Remove(term.IndexOf('+'), 1);
        }

        // if x is not found then return number 
        if (!term.Contains('x'))
        {
            if (double.TryParse(term, out double number))
            {
                if (isPositive)
                {
                    return number;
                }
                else
                {
                    return -number;
                }
            }
        }

        // if x is the only letter in equation
        if (term.Contains('x') && term.Length == 1)
        {
            if (isPositive)
            {
                return x;
            }
            else
            {
                return -x;
            }
        }

        // when term starts with x and has power 
        if (term.StartsWith("x^"))
        {
            if (double.TryParse(term[(term.IndexOf('^') + 1)..], out double power))
            {
                result = Math.Pow(x, power);

                if (isPositive)
                {
                    return result;
                }
                else
                {
                    return -result;
                }
            }
        }


        if (term.Contains("x^"))
        {
            double numberBeforeX = 1;

            // capture the number
            if (double.TryParse(term[..term.IndexOf('x')], out numberBeforeX))
            {
                term = term[term.IndexOf('x')..];

                if (double.TryParse(term[(term.IndexOf('^') + 1)..], out double power))
                {
                    result = numberBeforeX * Math.Pow(x, power);

                    if (isPositive)
                    {
                        return result;
                    }
                    else
                    {
                        return -result;
                    }
                }

            }
        }


        if (term.Contains('x'))
        {
            double numberBeforeX = 1;

            if (double.TryParse(term[..term.IndexOf('x')], out numberBeforeX))
            {
                result = numberBeforeX * x;

                if (isPositive)
                {
                    return result;
                }
                else
                {
                    return -result;
                }
            }
        }


        return result;
    }



    // Computes y given x
    public double ComputY(double x)
    {
        double y = 0.0;

        foreach (string term in terms)
        {
            y += computeTerm(term, x); 
        }

        return y;
    }

}

















