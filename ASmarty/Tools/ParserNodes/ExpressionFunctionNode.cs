using ASmarty.ViewEngine;

namespace ASmarty.Tools.ParserNodes
{
    internal class ExpressionFunctionNode : IParserNode
    {
        public int Id { get; private set; }
        public IExpressionFunction ExpressionFunction { get; private set; }
        public string Detail { get; private set; }
        public string Content { get; private set; }

        public ExpressionFunctionNode(int id, IExpressionFunction expressionFunction, string detail, string content)
        {
            Id = id;
            ExpressionFunction = expressionFunction;
            Detail = detail;
            Content = content;
        }
    }
}
