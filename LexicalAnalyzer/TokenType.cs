using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalyzer;
internal enum TokenType
{
    OPERATOR,
    IDENTIFIER,
    NUMBER,
    SEMICOLON,
    LBRACKET,
    RBRACKET,
    DIV,
    MOD
}
