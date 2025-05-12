using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationsOnConsoleGraph.Model; 

public class Equation
{
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


    // C
    //public double ComputY(double x)
    //{
    //    double y = 0; 

    //    foreach (string term in terms)
    //    {
    //        for (int i = 0; i < term.Length; i++)
    //        {

    //        }
    //    }
    //}
}
