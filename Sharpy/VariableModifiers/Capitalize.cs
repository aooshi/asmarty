using System;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Sharpy.ViewEngine;

namespace Sharpy.VariableModifiers
{
    [Export(typeof(IVariableModifier))]
    public class Capitalize : IVariableModifier
    {
        public object Evaluate(object input, IEvaluator evaluator, params object[] parameters)
        {
            var capitalizeWordsWithDigits = false;
            if (parameters.Length == 1)
            {
                capitalizeWordsWithDigits = (bool) parameters[0];
            }

            return input == null ? null : CapitalizeString(input.ToString(), capitalizeWordsWithDigits);
        }

        private static string CapitalizeString(string input, bool capitalizeWordsWithDigits)
        {
            string pattern;
            if (capitalizeWordsWithDigits)
            {
                pattern = @"\w+";
            }
            else
            {
                pattern = @"(?<![0-9])[A-Za-z_]+(?![0-9])";
            }
            
            return Regex.Replace(input, pattern, new MatchEvaluator(CapitalizeMatch));
        }

        private static string CapitalizeMatch(Match match)
        {
            return char.ToUpper(match.Value[0]) + match.Value.Substring(1, match.Value.Length - 1).ToLower();
        }

        public string Name
        {
            get { return "capitalize"; }
        }
    }
}
