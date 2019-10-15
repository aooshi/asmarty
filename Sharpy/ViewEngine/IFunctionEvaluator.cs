using System.Collections.Generic;

namespace Sharpy.ViewEngine
{
    public interface IFunctionEvaluator : IEvaluator
    {
        IDictionary<string, object> FunctionData { get; }
    }
}
