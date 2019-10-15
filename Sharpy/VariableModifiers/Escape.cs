using System.ComponentModel.Composition;
using ASmarty.ViewEngine;

namespace ASmarty.VariableModifiers
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
