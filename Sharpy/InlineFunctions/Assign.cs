using System.Collections.Generic;
using System.ComponentModel.Composition;
using Sharpy.Extensions;
using Sharpy.ViewEngine;

namespace Sharpy.InlineFunctions
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
