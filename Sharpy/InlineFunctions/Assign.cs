using System.Collections.Generic;
using System.ComponentModel.Composition;
using ASmarty.Extensions;
using ASmarty.ViewEngine;

namespace ASmarty.InlineFunctions
{
    [Export(typeof(IInlineFunction))]
    internal class Assign : IInlineFunction
    {
        public string Evaluate(IDictionary<string, object> attributes, IFunctionEvaluator evaluator)
        {
            var name = attributes.GetRequiredAttribute<string>("var");
            var value = attributes.GetRequiredAttribute<object>("value");

            evaluator.LocalData[name] = value;

            return null;
        }

        public string Name
        {
            get { return "assign"; }
        }
    }
}
