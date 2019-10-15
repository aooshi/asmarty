using System.ComponentModel.Composition;
using Sharpy.ViewEngine;

namespace Sharpy.VariableModifiers
{
    [Export(typeof(IVariableModifier))]
    public class Escape : IVariableModifier
    {
        public object Evaluate(object input, IEvaluator evaluator, params object[] parameters)
        {
            return input == null ? null : evaluator.Encode(input.ToString());
        }

        public string Name
        {
            get { return "escape"; }
        }
    }
}
