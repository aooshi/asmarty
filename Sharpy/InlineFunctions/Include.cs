using System.Collections.Generic;
using System.ComponentModel.Composition;
using Sharpy.ViewEngine;
using Sharpy.Extensions;

namespace Sharpy.InlineFunctions
{
    [Export(typeof(IInlineFunction))]
    public class Include : IInlineFunction
    {
        public string Evaluate(IDictionary<string, object> attributes, IFunctionEvaluator evaluator)
        {
            var file = attributes.GetRequiredAttribute<string>("file");
            var assign = attributes.GetOptionalAttribute<string>("assign");

            var evaluatedTemplate = evaluator.EvaluateTemplate(file);
            if (assign == null)
            {
                return evaluatedTemplate;
            }
            else
            {
                evaluator.LocalData.Add(assign, evaluatedTemplate);
                return null;
            }
        }

        public string Name
        {
            get { return "include"; }
        }
    }
}
