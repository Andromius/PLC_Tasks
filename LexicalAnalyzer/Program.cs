using System.Collections.ObjectModel;
using System.Reflection.PortableExecutable;

namespace LexicalAnalyzer;

internal class Program
{
    static readonly List<char> operators = ['+', '-', '/', '*'];
    static readonly string OPERATOR = "OPERATOR";
    static readonly string IDENTIFIER = "IDENTIFIER";
    static readonly string NUMBER = "NUMBER";
    static readonly Dictionary<char, string> delimiters = new()
    {
        {';', "SEMICOLON"},
        {'(', "LBRACKET"},
        {')', "RBRACKET"},
    };
    static readonly Dictionary<string, string> keywords = new()
    {
        {"div", "DIV"},
        {"mod", "MOD"}
    };
    static void Main(string[] args)
    {
        string[] input = File.ReadAllLines(args[0]);
        foreach (string line in input)
        {
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (char.IsWhiteSpace(c)) continue;
                else if (delimiters.TryGetValue(c, out string? value)) WriteToken(value);
                else if (char.IsLetter(c))
                {
                    string word = $"{c}";
                    i++;
                    while (i < line.Length && char.IsLetterOrDigit(line[i]))
                    {
                        word += line[i];
                        i++;
                    }
                    i--;
                    if (keywords.TryGetValue(word, out value)) WriteToken(value);
                    else WriteToken(IDENTIFIER, word);
                }
                else if (operators.Contains(c))
                {
                    if (i + 1 < line.Length && IsNote(c, line[i + 1])) break;
                    WriteToken(OPERATOR, c.ToString());
                }
                else if (char.IsDigit(c))
                {
                    string number = string.Empty;
                    while (i < line.Length && char.IsDigit(line[i]))
                    {
                        number += line[i];
                        i++;
                    }
                    i--;
                    WriteToken(NUMBER, number);
                }

            }
        }

    }
    static bool IsNote(char first, char second) => first == '/' && second == '/';
    static void WriteToken(string token, string value) => Console.WriteLine($"\u001b[35m{token}:\u001b[0m{value}");
    static void WriteToken(string token) => Console.WriteLine($"\u001b[35m{token}\u001b[0m");
}
