using System.Collections;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LexicalAnalyzer;
internal class Scanner(string path) : IEnumerable<Token>
{
    private string Text { get; } = File.ReadAllText(path);
    private readonly List<char> operators = ['+', '-', '/', '*'];
    private readonly Dictionary<char, TokenType> delimiters = new()
    {
        {';', TokenType.SEMICOLON},
        {'(', TokenType.LBRACKET},
        {')', TokenType.RBRACKET},
    };
    private readonly Dictionary<string, TokenType> keywords = new()
    {
        {"div", TokenType.DIV},
        {"mod", TokenType.MOD}
    };
    private Token? NextToken(ref int index)
    {
        char c = Text[index];
        if (char.IsWhiteSpace(c)) return null;
        else if (delimiters.TryGetValue(c, out TokenType value)) return new(value);
        else if (char.IsLetter(c))
        {
            string word = CreateWord(ref index);
            if (keywords.TryGetValue(word, out value)) return new(value);
            else return new(TokenType.IDENTIFIER, word);
        }
        else if (operators.Contains(c))
        {
            if (index + 1 < Text.Length && IsNote(c, Text[index + 1]))
            {
                index = Text.IndexOf('\n', index + 2);
                return null;
            }
            return new(TokenType.OPERATOR, c.ToString());
        }
        else if (char.IsDigit(c)) return new(TokenType.NUMBER, CreateNumber(ref index));
        return null;
    }
    private static bool IsNote(char first, char second) => first == '/' && second == '/';

    public IEnumerator<Token> GetEnumerator()
    {
        for (int i = 0; i < Text.Length; i++)
        {
            Token? token = NextToken(ref i);
            if (token is not null) yield return token;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private string CreateNumber(ref int index)
    {
        string number = $"{Text[index]}";
        index++;
        while (index < Text.Length && char.IsDigit(Text[index]))
        {
            number += Text[index];
            index++;
        }
        index--;
        return number;
    }

    private string CreateWord(ref int index)
    {
        string word = $"{Text[index]}";
        index++;
        while (index < Text.Length && char.IsLetterOrDigit(Text[index]))
        {
            word += Text[index];
            index++;
        }
        index--;
        return word;
    }
}
