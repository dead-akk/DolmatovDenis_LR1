using System.Collections.Generic;

namespace Calculator.Models
{
    public static class CalculatorHistory
    {
        public static List<string> Entries = new List<string>();

        public static void Add(string expression, string result)
        {
            Entries.Add($"{expression} = {result}");
        }
    }
}
