namespace Sharpy.ViewEngine
{
    public interface IExpressionFunction : IFunctionWithContent
    {
        string Evaluate(string functionDetails, IFunctionEvaluator evaluator, string content);
    }
}
