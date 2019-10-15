using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Sharpy.ViewEngine;

namespace Sharpy.VariableModifiers
{
    [Export(typeof(IVariableModifier))]
    public class CountParagraphs : IVariableModifier
    {
        public object Evaluate(object input, IEvaluator evaluator, params object[] parameters)
        {
            if (input == null)
            {
                return 0;
            }
            else
            {
                return Regex.Matches(input.ToString(), @"[\r\n]+").Count + 1;
            }
        }

        public string Name
        {
            get { return "count_paragraphs"; }
        }
    }
}
