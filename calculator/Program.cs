using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.RegularExpressions;

namespace calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //takes input and runs it through calculator function
            string input = Console.ReadLine();
            calculator(input);
        }
        static string[] checkDM(string input)
        {
            //splits while keeping delimiters
            string[] parts = Regex.Split(input, @"([-+*/])");
            //empty array for partially solved equation
            string[] equation = new string[parts.Length];

            //repeats for length of equation
            for (int i = 0; i < parts.Length; i++)
            {

                //completes detected multiplication and division, adds results to equation array, and changes involved values to null
                if (parts[i] == "/" && parts[i - 1] != null)
                {
                    float eq = float.Parse(parts[i - 1]) / float.Parse((parts[i + 1]));
                    equation[i] = eq.ToString();
                    parts[i] = null;
                    parts[i - 1] = null;
                    parts[i + 1] = null;
                }
                else if (parts[i] == "*" && parts[i - 1] != null)
                {
                    float eq = float.Parse(parts[i - 1]) * float.Parse(parts[i + 1]);
                    equation[i] = eq.ToString();
                    parts[i] = null;
                    parts[i - 1] = null;
                    parts[i + 1] = null;
                }
            }

            //takes in other values if not null and addds them to equation
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] != null)
                {
                    equation[i] = parts[i];
                }
            }
            return equation;
        }

        //does same thing but wath addition and subtraction
        static string[] checkAS(string input)
        {
            string[] parts = Regex.Split(input, @"([-+*/])");
            string[] equation = new string[parts.Length];
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] == "-" && parts[i - 1] != null)
                {
                    float eq = float.Parse(parts[i - 1]) - float.Parse((parts[i + 1]));
                    equation[i] = eq.ToString();
                    parts[i] = null;
                    parts[i - 1] = null;
                    parts[i + 1] = null;
                }
                else if (parts[i] == "+" && parts[i - 1] != null)
                {
                    float eq = float.Parse(parts[i - 1]) + float.Parse(parts[i + 1]);
                    equation[i] = eq.ToString();
                    parts[i] = null;
                    parts[i - 1] = null;
                    parts[i + 1] = null;
                }
            }
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] != null)
                {
                    equation[i] = parts[i];
                }
            }
            return equation;
        }

        
        static void calculator(string input)
        {
            
            string[] DMCheck = checkDM(input);
            //while equation still has multiplication and division symbols, it will repeatedly check for solvable division and multiplication
            while (DMCheck.Contains("/") || DMCheck.Contains("*"))
            {
                DMCheck= checkDM(string.Join("", DMCheck));
            }
            //same thing for addition and subtraction
            string[] ASCheck = DMCheck;
            while (ASCheck.Contains("+") || ASCheck.Contains("-"))
            {
                ASCheck = checkAS(string.Join("", ASCheck));
            }

            //turns array back to string and prints it
            string finalEquation = string.Join("", ASCheck);
            Console.WriteLine(finalEquation);
        }
    }
}
