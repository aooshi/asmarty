using System.ComponentModel.Composition;
using Sharpy.ViewEngine;

namespace Sharpy.VariableModifiers
{
    [Export(typeof(IVariableModifier))]
    public class Cat : IVariableModifier
    {
        public object Evaluate(object input, IEvaluator evaluator, params object[] parameters)
        {
            if (parameters.Length == 1)
            {
                var value = (string)parameters[0];
                return input == null ? value : input + value;
            }
            else
            {
                return input;
            }
        }

        public string Name
        {
            get { return "cat"; }
        }
    }
}
