using System.Globalization;

namespace ExpressionInterpreter;

internal class Program
{
    static readonly List<char> ops = ['+', '-', '*', '/'];
    static void Main(string[] args)
    {
        string line = Console.ReadLine()!;
        int lineCount = Convert.ToInt32(line);
        for (int i = 0; i < lineCount; i++)
        {
            Stack<char> operators = new();
            Stack<int> numbers = new();
            string input = Console.ReadLine()!;
            Console.WriteLine(CheckExpression(operators, numbers, input));   
        }
    }

    public static bool IsNumber(char c) => c >= '0' && c <= '9';
    public static int Evaluate(int a, int b, char op)
    {
        return op switch
        {
            '+' => b + a,
            '-' => b - a,
            '*' => b * a,
            '/' => b / a,
            _ => throw new ArgumentException("invalid operator")
        };
    }
    public static int GetPrecedence(char op)
    {
        return op switch
        {
            '+' or '-' => 1,
            '*' or '/' => 2,
            _ => 0,
        };
    }

    public static string CheckExpression(Stack<char> operators, Stack<int> numbers, string input)
    {
        for (int j = 0; j < input.Length; j++)
        {
            char c = input[j];
            if (c == ' ') continue;
            string number = string.Empty;
            if (IsNumber(c))
            {
                while (j < input.Length && IsNumber(input[j]))
                {
                    number += input[j];
                    j++;
                }

                if (number.First() == '0') return "ERROR";
                numbers.Push(Convert.ToInt32(number));
                j--;
            }
            else if (c == '(')
            {
                operators.Push(c);
            }
            else if (c == ')')
            {
                while (operators.Peek() != '(')
                {
                    numbers.Push(Evaluate(numbers.Pop(), numbers.Pop(), operators.Pop()));
                }
                operators.Pop();
            }
            else if (ops.Contains(c))
            {
                while (operators.Count > 0 && GetPrecedence(operators.Peek()) >= GetPrecedence(c))
                {
                    numbers.Push(Evaluate(numbers.Pop(), numbers.Pop(), operators.Pop()));
                }
                operators.Push(c);
            }
        }

        int result = 0;
        while (operators.TryPop(out char op))
        {
            if (numbers.TryPop(out int a) && numbers.TryPop(out int b))
                result += Evaluate(a, b, op);
            else result = -1;
        }

        if (result == -1 || operators.Count > 0) return "ERROR";
        else return Convert.ToString(result);
    }
}
