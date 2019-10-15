using System.Collections.Generic;

namespace Sharpy.ViewEngine
{
    public interface IBlockFunction : IFunctionWithContent
    {
        string Evaluate(IDictionary<string, object> attributes, IFunctionEvaluator evaluator, string content);
    }
}