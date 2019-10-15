using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Sharpy.ViewEngine;

namespace Sharpy.VariableModifiers
{
    [Export(typeof(IVariableModifier))]
    public class Strip : IVariableModifier
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
                if (parameters.Length == 1)
                {
                    replaceCharacter = (string) parameters[0];
                }
                return Regex.Replace(input.ToString(), @"\s+", replaceCharacter);
            }
        }

        public string Name
        {
            get { return "strip"; }
        }
    }
}
