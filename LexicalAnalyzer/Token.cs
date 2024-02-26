using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalyzer;
internal class Token
{
    public TokenType TokenType { get; }
    public string Value { get; }
    public Token (TokenType tokenType, string value)
    {
        TokenType = tokenType;
        Value = value;
    }
    public Token(TokenType tokenType)
    {
        TokenType = tokenType;
        Value = string.Empty;
    }

    public override string ToString()
    {
        return string.IsNullOrEmpty(Value) ? $"\u001b[35m{TokenType}\u001b[0m" : $"\u001b[35m{TokenType}:\u001b[0m{Value}";
    }
}
