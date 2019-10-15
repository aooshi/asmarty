using System.Collections.Generic;

namespace ASmarty.ViewEngine
{
    public interface IInlineFunction : INamedExport
    {
        string Evaluate(IDictionary<string, object> attributes, IFunctionEvaluator evaluator);
    }
}
