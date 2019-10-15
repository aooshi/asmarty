namespace Sharpy.ViewEngine
{
    public enum PartType
    {
        String,
        Expression
    }

    public class ExpressionPart
    {
        public string Detail { get; set; }
        public PartType Type { get; private set; }

        public ExpressionPart(string detail, PartType type)
        {
            Detail = detail;
            Type = type;
        }
    }
}
