using System.Collections.Generic;

namespace ASmarty.Tools.ParserNodes
{
    internal class ExpressionNode : IParserNode
    {
        public int Id { get; private set; }
        public string Detail { get; private set; }
        public IDictionary<string, IList<ExpressionNode>> Modifiers { get; set; }

        public ExpressionNode(int id, string detail, IDictionary<string, IList<ExpressionNode>> modifiers)
        {
            Id = id;
            Detail = detail;
            Modifiers = modifiers;
        }
    }
}
