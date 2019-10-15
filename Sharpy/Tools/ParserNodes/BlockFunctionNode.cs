using System.Collections.Generic;
using ASmarty.ViewEngine;

namespace ASmarty.Tools.ParserNodes
{
    internal class BlockFunctionNode : IParserNode
    {
        public int Id { get; private set; }
        public IBlockFunction BlockFunction { get; private set; }
        public IDictionary<string, ExpressionNode> Attributes { get; private set; }
        public string Content { get; private set; }

        public BlockFunctionNode(int id, IBlockFunction blockFunction, IDictionary<string, ExpressionNode> attributes, string content)
        {
            Id = id;
            BlockFunction = blockFunction;
            Attributes = attributes;
            Content = content;
        }
    }
}
