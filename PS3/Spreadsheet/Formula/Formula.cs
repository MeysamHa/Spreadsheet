// Skeleton written by Joe Zachary for CS 3500, January 2015
// Revised by Joe Zachary, January 2016
// JLZ Repaired pair of mistakes, January 23, 2016

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
//h
namespace Formulas
{

     /// <summary>
    /// Represents formulas written in standard infix notation using standard precedence
    /// rules.  Provides a means to evaluate Formulas.  Formulas can be composed of
    /// non-negative floating-point numbers, variables, left and right parentheses, and
    /// the four binary operator symbols +, -, *, and /.  (The unary operators + and -
    /// are not allowed.)
    /// </summary>
    public class Formula
    {
        //
        private IEnumerable<string> correctTokens;
       // keep track of normalize
          private HashSet<string> tokens;

        /// <summary>
        /// Creates a Formula from a string that consists of a standard infix expression composed
        /// from non-negative floating-point numbers (using C#-like syntax for double/int literals), 
        /// variable symbols (a letter followed by zero or more letters and/or digits), left and right
        /// parentheses, and the four binary operator symbols +, -, *, and /.  White space is
        /// permitted between tokens, but is not required.
        /// 
        /// Examples of a valid parameter to this constructor are:
        ///     "2.5e9 + x5 / 17"
        ///     "(5 * 2) + 8"
        ///     "x*y-2+35/9"
        ///     
        /// Examples of invalid parameters are:
        ///     "_"
        ///     "-5.3"
        ///     "2 5 + 3"
        /// 
        /// If the formula is syntacticaly invalid, throws a FormulaFormatException with an 
        /// explanatory Message.
        /// </summary>
        public Formula(String formula)
        {
        
            //Basic : Formula !=null || formula.Length > 0
            if (!String.IsNullOrEmpty(formula))
            {
                

               

                if (Regex.IsMatch(formula, @"^[\s]{1,}$"))
                    throw new FormulaFormatException("Nothing but whitespace was provided. Provide a parsable formula.");

                else if (Regex.IsMatch(formula, @"^[+*/)-]"))
                    throw new FormulaFormatException("Incorrect operator at the beginning. Remove and try again.");

                else if (formula == "")
                    throw new FormulaFormatException("No formula provided. Provide a parsable formula.");

                else if (Regex.IsMatch(formula, @"\d[\s][(]"))
                    throw new FormulaFormatException("Missing operator before paranthesis. Correct and try again.");

                else if (Regex.IsMatch(formula, @"\)[\s]*(\d)"))
                    throw new FormulaFormatException("Missing operator after paranthesis. Correct and try again.");

                else if (Regex.IsMatch(formula, @"(\+|\-|\*|\/)[\s]*(\+|\-|\*|\/)"))
                    throw new FormulaFormatException("Two consecutive operators. Correct and try again.");

                else if (Regex.IsMatch(formula, @"\b[\d](([a-d]|[f-z]|[A-D]|[F-Z]|_){1,})"))
                    throw new FormulaFormatException("Incorrect variable format. Correct and try again.");

                else if (Regex.IsMatch(formula, @"[^\d-+/*)(a-zA-Z\s\.]"))
                    throw new FormulaFormatException("Formula contains invalid character. Correct and try again.");

                else if (Regex.IsMatch(formula, @"\([\s]*[+/*-]{1,}"))
                    throw new FormulaFormatException("Operator right after left paranthesis. Correct and try again.");

                else if (Regex.IsMatch(formula, @"((\+|\-|\*|\/)(\s)*\))"))
                    throw new FormulaFormatException("Operator right before right paranthesis. Correct and try again.");

                else if (Regex.IsMatch(formula, @"\.\d*\."))
                    throw new FormulaFormatException("Too many decimal points.");

                else if (Regex.IsMatch(formula, @"\s\.*\s"))
                    throw new FormulaFormatException("Space in ");

                else if (Regex.IsMatch(formula, @"[-+*\\]$"))
                    throw new FormulaFormatException("Operator at the end");

                else if (Regex.IsMatch(formula, @"(\d)[\s](\d)"))
                    throw new FormulaFormatException("Missing operator between numbers");

                //.....................................
                //
                // 
                //throw new FormulaFormatException("Input cannot be null.");
                //GetTokens(formula);

                int totalleftparcount = 0;
                int totalrightparcount = 0;

                correctTokens = GetTokens(formula);
                foreach (string s in correctTokens)
                {
                    if (s == "(")
                    {

                        totalleftparcount++;

                    }
                    else if (s == ")")
                    {

                        totalrightparcount++;
                    }
                }


                if (totalleftparcount != totalrightparcount)
                {
                    throw new FormulaFormatException("Total left parentheses must be equal to total right parentheses");
                }


            }
            else
            {
                throw new FormulaFormatException("Input cannot be null or empty.");
            }

        }

        /// <summary>
        /// Helper method to analyze the precedence of a given token to the last operator of the stack.
        /// If the given token is multiplicaiton or division and the last operator of stack is addition or
        /// subtraction return false, meaning the stack has lower precedence.
        /// </summary>
        /// <param name="token">First operator</param>
        /// <param name="StackPeek">Second operator</param>
        /// <returns>True if stack operator has a higher precedence. False otherwise </returns>
        private static bool hasPrecedence(string token, string StackPeek)
        {
            // 
            if ((token == "*" || token == "/") && (StackPeek == "+" || StackPeek == "-"))
                return false;
            if (StackPeek == "(" || StackPeek == ")")
                return false;
            else
                return true;
        }

        /// <summary>
        /// Evaluates this Formula, using the lookup delegate to determine the values of
        /// variables.  When a variable symbol v needs to be determined, it should be looked up
        /// via lookup(normalize(v)). (Here, normalize is the normalizer that was passed to 
        /// the constructor.)
        /// 
        /// For example, if L("x") is 2, L("X") is 4, and N is a method that converts all the letters 
        /// in a string to upper case:
        /// 
        /// new Formula("x+7", N, s => true).Evaluate(L) is 11
        /// new Formula("x+7").Evaluate(L) is 9
        /// 
        /// Given a variable symbol as its parameter, lookup returns the variable's value 
        /// (if it has one) or throws an ArgumentException (otherwise).
        /// 
        /// If no undefined variables or divisions by zero are encountered when evaluating 
        /// this Formula, the value is returned.  Otherwise, a FormulaError is returned.  
        /// The Reason property of the FormulaError should have a meaningful explanation.
        ///
        /// This method should never throw an exception.
        /// </summary>
        public double Evaluate(Func<string, double> lookup)
        {
            // Stack for holding values and operatorrs
            Stack<double> valStack = new Stack<double>();
            Stack<string> optStack = new Stack<string>();

            foreach (string token in correctTokens)
            {
                if (token == "(")
                    optStack.Push(token);

                // Check token, opertator stack and operate appropriately based on precedence
                else if (token == "*" || token == "/" || token == "+" || token == "-")
                {
                    while (optStack.Count != 0 && hasPrecedence(token, optStack.Peek()))
                    {
                        try
                        {
                            double last = valStack.Pop();
                            double calculated;
                            calculated = optStack.applyOpt(valStack.Pop(), last);
                            valStack.Push(calculated);
                        }
                        catch
                        {
                            throw new FormulaEvaluationException("");
                        }

                    }
                    optStack.Push(token);
                }

                // If token is ")" apply operations until "(" is reached
                else if (token == ")")
                {
                    while (optStack.Peek() != "(")
                    {
                        double last = valStack.Pop();
                        double calculated;
                        calculated = optStack.applyOpt(valStack.Pop(), last);
                        valStack.Push(calculated);
                    }
                    optStack.Pop();
                }

                else if (isNum(token))
                {
                    // Respect precedence
                    if (optStack.isMultDiv())
                    {
                        double calculated;
                        calculated = optStack.applyOpt(valStack.Pop(), getNumberFromString(token));
                        valStack.Push(calculated);
                    }
                    else valStack.Push(getNumberFromString(token));
                }

                // Lastly, if all options were failed, we are left with a variable.
                else
                {
                    try
                    {
                        lookup(token);
                    }
                    catch
                    {
                        throw new FormulaEvaluationException("Invalid Variable");
                    }

                    // Respect precedence
                    if (optStack.isMultDiv())
                    {
                        double calculated;
                        calculated = optStack.applyOpt(valStack.Pop(), lookup(token));
                        valStack.Push(calculated);
                    }
                    else valStack.Push(lookup(token));
                }
            }

            double final;

            // Finish the remianing operations
            while (optStack.Count != 0 && valStack.Count != 0)
            {
                double last = valStack.Pop();
                double calculated;
                calculated = optStack.applyOpt(valStack.Pop(), last);
                valStack.Push(calculated);
            }

            if (valStack.Count == 0)
                throw new Exception();

            final = valStack.Pop();
            return final;
        }

        /// <summary>
        /// Function to convert a given string to a number
        /// </summary>
        /// <param name="s">String to be converted to appropriate number</param>
        /// <returns>Returns the value of the string as a double</returns>
        private static double getNumberFromString(string s)
        {
            double n;
            bool isnumber = double.TryParse(s, out n);
            return n;
        }

        /// <summary>
        /// Function to analyze if a string can be represented as a number
        /// </summary>
        /// <param name="s">String to be assessed</param>
        /// <returns>True if the string is a number, false otherwise</returns>
        private static bool isNum(string s)
        {
            double n;
            return double.TryParse(s, out n);
        }

        /// <summary>
        /// Given a formula, enumerates the tokens that compose it.  Tokens are left paren,
        /// right paren, one of the four operator symbols, a string consisting of a letter followed by
        /// zero or more digits and/or letters, a double literal, and anything that doesn't
        /// match one of those patterns.  There are no empty tokens, and no token contains white space.
        /// </summary>
        private static IEnumerable<string> GetTokens(String formula)
        {
            // Patterns for individual tokens
            String lpPattern = @"\(";
            String rpPattern = @"\)";
            String opPattern = @"[\+\-*/]";
            String varPattern = @"[a-zA-Z][0-9a-zA-Z]*";
            String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: e[\+-]?\d+)?";
            String spacePattern = @"\s+";

            // Overall pattern
            String pattern = String.Format("({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                            lpPattern, rpPattern, opPattern, varPattern, doublePattern, spacePattern);

            // Enumerate matching tokens that don't consist solely of white space.
            foreach (String s in Regex.Split(formula, pattern, RegexOptions.IgnorePatternWhitespace))
            {
                if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
                {
                    yield return s;

                }

              
         
            }
        }
    }

    /// <summary>
    /// A Lookup method is one that maps some strings to double values.  Given a string,
    /// such a function can either return a double (meaning that the string maps to the
    /// double) or throw an UndefinedVariableException (meaning that the string is unmapped 
    /// to a value. Exactly how a Lookup method decides which strings map to doubles and which
    /// don't is up to the implementation of the method.
    /// </summary>
    public delegate double Lookup(string s);

    /// <summary>
    /// Used to report that a Lookup delegate is unable to determine the value
    /// of a variable.
    /// </summary>
    public class UndefinedVariableException : Exception
    {
        /// <summary>
        /// Constructs an UndefinedVariableException containing whose message is the
        /// undefined variable.
        /// </summary>
        /// <param name="variable"></param>
        public UndefinedVariableException(String variable)
            : base(variable)
        {
        }
    }

    /// <summary>
    /// Used to report syntactic errors in the parameter to the Formula constructor.
    /// </summary>
    public class FormulaFormatException : Exception
    {
        /// <summary>
        /// Constructs a FormulaFormatException containing the explanatory message.
        /// </summary>
        public FormulaFormatException(String message) : base(message)
        {
        }
    }

    /// <summary>
    /// Used to report errors that occur when evaluating a Formula.
    /// </summary>
    public class FormulaEvaluationException : Exception
    {
        /// <summary>
        /// Constructs a FormulaEvaluationException containing the explanatory message.
        /// </summary>
        public FormulaEvaluationException(String message) : base(message)
        {
        }
    }
    public static class Extensions
    {

        /// <summary>
        /// Determines if the last operator on the stack is a multiply or divide
        /// </summary>
        /// <param name="st">Given stack to analyze</param>
        /// <returns>True if stack's top operator is multiply or divide. False otherwise</returns>
        public static bool isMultDiv(this Stack<string> st)
        {
            if (st.Count != 0)
                if (st.Peek() == "*" || st.Peek() == "/")
                    return true;
            return false;
        }

        /// <summary>
        /// Extension method of stacks to pop the operator and apply the operation to the two given parameters. Eg. firstParam - secondParam
        /// </summary>
        /// <param name="st">Given stack to use for operation.</param>
        /// <param name="a">First value in the mathematical expression</param>
        /// <param name="b">Second value in the mathematical expression</param>
        /// <returns></returns>
        public static double applyOpt(this Stack<string> st, double a, double b)
        {
            // Make sure the operator stack is not empty, proceed with appropriate operation and pop the operator from stack.
            if (st.Count != 0)
            {
                if (st.Peek() == "*")
                {
                    st.Pop();
                    return a * b;
                }
                if (st.Peek() == "/")
                {
                    st.Pop();
                    if (b == 0)
                    {
                        throw new FormulaEvaluationException("Division by zero");
                    }
                    return a / b;
                }
                if (st.Peek() == "+")
                {
                    st.Pop();
                    return a + b;
                }
                else
                {
                    st.Pop();
                    return a - b;
                }
            }
            return 0;
        }
    }
}
