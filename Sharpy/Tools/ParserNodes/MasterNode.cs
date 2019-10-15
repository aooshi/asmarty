using System.Collections.Generic;

namespace Sharpy.Tools.ParserNodes
{
    internal class MasterNode : IParserNode
    {
        public int Id { get; private set; }
        public IDictionary<string, ExpressionNode> Attributes { get; private set; }
        public IEnumerable<IParserNode> Content { get; private set; }

        public MasterNode(int id, IDictionary<string, ExpressionNode> attributes, IEnumerable<IParserNode> content)
        {
            Id = id;
            Attributes = attributes;
            Content = content;
        }
    }
}
