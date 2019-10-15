using System.ComponentModel.Composition;
using ASmarty.ViewEngine;

namespace ASmarty.VariableModifiers
{
    [Export(typeof(IVariableModifier))]
    public class Default : IVariableModifier
    {
        public object Evaluate(object input, IEvaluator evaluator, params object[] parameters)
        {
            var inputAsString = input == null ? null : input.ToString();
            if (string.IsNullOrEmpty(inputAsString) && parameters.Length == 1)
            {
                return parameters[0];
            }
            else
            {
                return input;
            }
        }

        public string Name
        {
            get { return "default"; }
        }
    }
}
