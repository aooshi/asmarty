using System.Collections.Generic;

namespace Sharpy.ViewEngine
{
    public interface IInlineFunction : INamedExport
    {
        string Evaluate(IDictionary<string, object> attributes, IFunctionEvaluator evaluator);
    }
}
