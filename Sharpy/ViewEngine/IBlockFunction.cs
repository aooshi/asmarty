using System.Collections.Generic;

namespace ASmarty.ViewEngine
{
    public interface IBlockFunction : IFunctionWithContent
    {
        string Evaluate(IDictionary<string, object> attributes, IFunctionEvaluator evaluator, string content);
    }
}