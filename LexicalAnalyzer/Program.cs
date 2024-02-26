using System.Collections.ObjectModel;
using System.Reflection.PortableExecutable;

namespace LexicalAnalyzer;

internal class Program
{
    static void Main(string[] args)
    {
        Scanner scanner = new(args[0]);
        foreach (var token in scanner) Console.WriteLine(token);
    }
}
