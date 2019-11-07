using System.ComponentModel.Composition;
using ASmarty.ViewEngine;

namespace ASmarty.VariableModifiers
{
    [Export(typeof(IVariableModifier))]
    public class Content : IVariableModifier
    {
        public object Evaluate(object input, IEvaluator evaluator, params object[] parameters)
        {
            string u = input.ToString();//.TrimEnd('\"');
            return evaluator.EvaluateUrl(u);
        }

        public string Name
        {
            get { return "content"; }
        }
    }
}
