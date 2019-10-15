namespace ASmarty.ViewEngine
{
    public interface IVariableModifier : INamedExport
    {
        object Evaluate(object input, IEvaluator evaluator, params object[] parameters);
    }
}
