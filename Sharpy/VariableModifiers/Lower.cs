using System.ComponentModel.Composition;
using ASmarty.ViewEngine;

namespace ASmarty.VariableModifiers
{
    [Export(typeof(IVariableModifier))]
    public class Lower : IVariableModifier
    {
        public object Evaluate(object input, IEvaluator evaluator, params object[] parameters)
        {
            return input == null ? null : input.ToString().ToLower();
        }

        public string Name
        {
            get { return "lower"; }
        }
    }
}
