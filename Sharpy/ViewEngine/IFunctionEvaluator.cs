using System.Collections.Generic;

namespace ASmarty.ViewEngine
{
    public interface IFunctionEvaluator : IEvaluator
    {
        IDictionary<string, object> FunctionData { get; }
    }
}
