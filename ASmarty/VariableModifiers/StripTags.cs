using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using ASmarty.ViewEngine;

namespace ASmarty.VariableModifiers
{
    [Export(typeof(IVariableModifier))]
    public class StripTags : IVariableModifier
    {
        public object Evaluate(object input, IEvaluator evaluator, params object[] parameters)
        {
            if (input == null)
            {
                return null;
            }
            else
            {
                var replaceCharacter = " ";
                if (parameters.Length == 1 && (bool)parameters[0] == false)
                {
                    replaceCharacter = "";
                }
                return Regex.Replace(input.ToString(), @"<[^>]*?>", replaceCharacter);
            }
        }

        public string Name
        {
            get { return "strip_tags"; }
        }
    }
}
