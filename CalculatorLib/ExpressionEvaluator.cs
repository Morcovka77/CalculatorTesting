using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CalculatorLib
{
    /// <summary>
    /// Evaluates mathematical expressions with proper operator precedence.
    /// Supports addition, subtraction, multiplication, and division.
    /// </summary>
    public class ExpressionEvaluator
    {
        private readonly Calculator _calculator;
        
        /// <summary>
        /// Initializes a new instance of the ExpressionEvaluator class.
        /// </summary>
        public ExpressionEvaluator()
        {
            _calculator = new Calculator();
        }

        /// <summary>
        /// Evaluates a given mathematical expression respecting operator precedence.
        /// </summary>
        /// <param name="expression">The expression to evaluate (e.g., "2 + 3 * 4")</param>
        /// <returns>The result of the evaluation</returns>
        /// <exception cref="ArgumentException">Thrown when the expression is invalid</exception>
        /// <exception cref="DivideByZeroException">Thrown when attempting to divide by zero</exception>
        public double Evaluate(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                throw new ArgumentException("Expression cannot be empty", nameof(expression));
            }

            // Special case for single negative number
            if (expression.Trim().StartsWith("-") && !expression.Trim().Contains(" "))
            {
                if (double.TryParse(expression, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
                {
                    return result;
                }
            }

            // Tokenize the expression
            var tokens = Tokenize(expression);
            
            // Convert infix notation to postfix (Reverse Polish Notation)
            var postfix = ConvertToPostfix(tokens);
            
            // Evaluate the postfix expression
            return EvaluatePostfix(postfix);
        }

        private List<string> Tokenize(string expression)
        {
            var tokens = new List<string>();
            var currentToken = new StringBuilder();
            
            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];
                
                // Skip whitespace
                if (char.IsWhiteSpace(c))
                {
                    if (currentToken.Length > 0)
                    {
                        tokens.Add(currentToken.ToString());
                        currentToken.Clear();
                    }
                    continue;
                }
                
                // Handle special case for negative numbers at the beginning or after another operator
                if (c == '-' && (i == 0 || IsOperator(tokens.LastOrDefault()) || tokens.LastOrDefault() == "("))
                {
                    // This is a negative sign for a number, not a subtraction operator
                    currentToken.Append(c);
                }
                // If the current character is an operator
                else if (IsOperator(c.ToString()))
                {
                    // Add the current number token if any
                    if (currentToken.Length > 0)
                    {
                        tokens.Add(currentToken.ToString());
                        currentToken.Clear();
                    }
                    
                    // Add the operator token
                    tokens.Add(c.ToString());
                }
                // If the current character is a digit or decimal point
                else if (char.IsDigit(c) || c == '.')
                {
                    currentToken.Append(c);
                }
                // If the current character is a parenthesis
                else if (c == '(' || c == ')')
                {
                    // Add the current number token if any
                    if (currentToken.Length > 0)
                    {
                        tokens.Add(currentToken.ToString());
                        currentToken.Clear();
                    }
                    
                    // Add the parenthesis token
                    tokens.Add(c.ToString());
                }
                else
                {
                    throw new ArgumentException($"Invalid character in expression: {c}");
                }
            }
            
            // Add the last token if any
            if (currentToken.Length > 0)
            {
                tokens.Add(currentToken.ToString());
            }
            
            return tokens;
        }

        private List<string> ConvertToPostfix(List<string> infixTokens)
        {
            var output = new List<string>();
            var operatorStack = new Stack<string>();
            
            foreach (var token in infixTokens)
            {
                // If token is a number, add it to the output
                if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
                {
                    output.Add(token);
                }
                // If token is an operator
                else if (IsOperator(token))
                {
                    // While there is an operator at the top of the stack with higher precedence
                    while (operatorStack.Count > 0 && 
                           IsOperator(operatorStack.Peek()) && 
                           GetPrecedence(operatorStack.Peek()) >= GetPrecedence(token))
                    {
                        output.Add(operatorStack.Pop());
                    }
                    
                    operatorStack.Push(token);
                }
                // If token is a left parenthesis, push it onto the stack
                else if (token == "(")
                {
                    operatorStack.Push(token);
                }
                // If token is a right parenthesis
                else if (token == ")")
                {
                    // Until the token at the top of the stack is a left parenthesis,
                    // pop operators from the stack onto the output queue
                    while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
                    {
                        output.Add(operatorStack.Pop());
                    }
                    
                    // Pop the left parenthesis from the stack, but not onto the output queue
                    if (operatorStack.Count > 0 && operatorStack.Peek() == "(")
                    {
                        operatorStack.Pop();
                    }
                    else
                    {
                        throw new ArgumentException("Mismatched parentheses");
                    }
                }
                else
                {
                    throw new ArgumentException($"Invalid token: {token}");
                }
            }
            
            // Pop any remaining operators from the stack onto the output queue
            while (operatorStack.Count > 0)
            {
                var op = operatorStack.Pop();
                if (op == "(")
                {
                    throw new ArgumentException("Mismatched parentheses");
                }
                output.Add(op);
            }
            
            return output;
        }

        private double EvaluatePostfix(List<string> postfixTokens)
        {
            var stack = new Stack<double>();
            
            foreach (var token in postfixTokens)
            {
                // If token is a number, push it onto the stack
                if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out double number))
                {
                    stack.Push(number);
                }
                // If token is an operator
                else if (IsOperator(token))
                {
                    // There should be at least two numbers on the stack
                    if (stack.Count < 2)
                    {
                        throw new ArgumentException("Invalid expression");
                    }
                    
                    // Pop the two numbers from the stack
                    double b = stack.Pop();
                    double a = stack.Pop();
                    
                    // Perform the operation
                    double result = ApplyOperator(token, a, b);
                    
                    // Push the result back onto the stack
                    stack.Push(result);
                }
                else
                {
                    throw new ArgumentException($"Invalid token: {token}");
                }
            }
            
            // The final result should be the only number on the stack
            if (stack.Count != 1)
            {
                throw new ArgumentException("Invalid expression");
            }
            
            return stack.Pop();
        }

        private bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/";
        }

        private int GetPrecedence(string op)
        {
            return op switch
            {
                "+" => 1,
                "-" => 1,
                "*" => 2,
                "/" => 2,
                _ => 0
            };
        }

        private double ApplyOperator(string op, double a, double b)
        {
            return op switch
            {
                "+" => _calculator.Add(a, b),
                "-" => _calculator.Subtract(a, b),
                "*" => _calculator.Multiply(a, b),
                "/" => _calculator.Divide(a, b),
                _ => throw new ArgumentException($"Unknown operator: {op}")
            };
        }
    }
}
