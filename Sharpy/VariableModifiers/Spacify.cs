using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Sharpy.ViewEngine;

namespace Sharpy.VariableModifiers
{
    [Export(typeof(IVariableModifier))]
    public class Spacify : IVariableModifier
    {
        public object Evaluate(object input, IEvaluator evaluator, params object[] parameters)
        {
            if (input == null)
            {
                return null;
            }
            else
            {
                var inputAsString = input.ToString();
                var joinCharacter = " ";
                if (parameters.Length == 1)
                {
                    joinCharacter = (string) parameters[0];
                }

                return inputAsString[0] + string.Join(joinCharacter, Regex.Split(inputAsString.Substring(1, inputAsString.Length - 2), "")) + inputAsString[inputAsString.Length - 1];
            }
        }

        public string Name
        {
            get { return "spacify"; }
        }
    }
}
