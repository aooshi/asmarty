using System.ComponentModel.Composition;
using Sharpy.ViewEngine;

namespace Sharpy.VariableModifiers
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
